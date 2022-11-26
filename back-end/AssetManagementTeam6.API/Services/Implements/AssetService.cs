using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Implements
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ICategoryRepository _categoryRepository;
        public AssetService(IAssetRepository assetRepository, ICategoryRepository categoryRepository)
        {
            _assetRepository = assetRepository;
            _categoryRepository = categoryRepository;
        }
            
        public async Task<Asset?> Create(AssetRequest asset)
        {
            var newAsset = new Asset
            {
                AssetName = asset.AssetName,
                CategoryId = asset.CategoryId,
                Category = await _categoryRepository.GetOneAsync(x => x.Id == asset.CategoryId),
                State = asset.State,
                InstalledDate = asset.InstalledDate,
                Specification = asset.Specification,
                Location = asset.Location
            };

            var createdAsset = await _assetRepository.Create(newAsset);

            if (createdAsset == null)
            {
                return null;
            }

            return createdAsset;
        }

        public async Task<IEnumerable<GetAssetResponse>> GetAllAsync(LocationEnum location)
        {
            var assets = await _assetRepository.GetListAsync();

            var assetByLocation = assets.Where(x => x.Location == location);

            return assetByLocation.Select(asset => new GetAssetResponse(asset));
        }

        public async Task<Asset?> GetAssetByName(string assetName)
        {
            return await _assetRepository.GetOneAsync(asset => asset.AssetName == assetName);
        }

        public async Task<bool> Delete(int id)
        {
            var deleteasset = await _assetRepository.GetOneAsync(asset => asset.Id == id);

            if (deleteasset == null)
            {
                return false;
            }

            await _assetRepository.Delete(deleteasset);

            return true;
        }

        public async Task<Pagination<GetAssetResponse?>> GetPagination(PaginationQueryModel queryModel, LocationEnum location)
        {
            // TODO: get list assets not set with location
            var assets = await _assetRepository.GetListAsync(x => x.Location == location);

            // filter by type
            if (queryModel.State != null)
            {
                assets = assets?.Where(u => queryModel.State.Contains(u.State))?.ToList();
            }

            if (queryModel.Category != null)
            {
                assets = assets.Where(u => queryModel.Category.Contains(u.CategoryId))?.ToList();
            }

            // search asset by staffcode or fullname
            var nameToQuery = "";
            if (!string.IsNullOrEmpty(queryModel.StaffCodeOrName))
            {
                nameToQuery = queryModel.StaffCodeOrName.Trim().ToLower();
                assets = assets?.Where(u => u!.AssetName!.ToLower().Contains(nameToQuery) ||
                                        u!.AssetCode!.ToLower().Contains(nameToQuery))?.ToList();
            }

            //sorting
            var sortOption = queryModel.Sort ??= Constants.AssetCodeAcsending;

            switch (sortOption)
            {
                case Constants.AssetNameAcsending:
                    assets = assets?.OrderBy(u => u.AssetName)?.ToList();
                    break;
                case Constants.AssetNameDescending:
                    assets = assets?.OrderByDescending(u => u.AssetName)?.ToList();
                    break;
                case Constants.AssetCodeAcsending:
                    assets = assets?.OrderBy(u => u.AssetCode)?.ToList();
                    break;
                case Constants.AssetCodeDescending:
                    assets = assets?.OrderByDescending(u => u.AssetCode)?.ToList();
                    break;
                case Constants.AssetInstalledDateAcsending:
                    assets = assets?.OrderBy(u => u.InstalledDate)?.ToList();
                    break;
                case Constants.AssetInstalledDateDescending:
                    assets = assets?.OrderByDescending(u => u.InstalledDate)?.ToList();
                    break;
                case Constants.AssetStateAcsending:
                    assets = assets?.OrderBy(u => u.State)?.ToList();
                    break;
                case Constants.AssetStateDescending:
                    assets = assets?.OrderByDescending(u => u.State)?.ToList();
                    break;
                default:
                    assets = assets?.OrderBy(u => u.AssetCode)?.ToList();
                    break;
            }

            //paging
            if (assets == null || assets.Count() == 0)
            {
                return new Pagination<GetAssetResponse?>
                {
                    Source = null,
                    TotalPage = 1,
                    TotalRecord = 0,
                    QueryModel = queryModel
                };
            }

            var output = new Pagination<GetAssetResponse>();

            output.TotalRecord = assets.Count();

            var listassets = assets.Select(asset => new GetAssetResponse(asset));

            output.Source = listassets.Skip((queryModel.Page - 1) * queryModel.PageSize)
                                    .Take(queryModel.PageSize)
                                    .ToList();

            output.TotalPage = (output.TotalRecord - 1) / queryModel.PageSize + 1;

            if (queryModel.Page > output.TotalPage)
            {
                queryModel.Page = output.TotalPage;
            }

            output.QueryModel = queryModel;

            return output;
        }

        public async Task<Asset?> Update(AssetRequest updateRequest)
        {
            var updatedAssert =await _assetRepository.GetOneAsync(x => x.Id ==updateRequest.Id);
            if (updatedAssert == null) return null;

            {
                updatedAssert.Location = updateRequest.Location;    
                updatedAssert.AssetName = updateRequest.AssetName;
                updatedAssert.InstalledDate = updateRequest.InstalledDate;
                updatedAssert.State = updateRequest.State;
                updatedAssert.CategoryId = updateRequest.CategoryId;
                updatedAssert.Specification = updateRequest.Specification;
            }
                
            return await _assetRepository.Update(updatedAssert);
        }

        public async Task<Asset> GetAssetById(int id)
        {
            return await _assetRepository.GetOneAsync(a => a.Id == id);
        }
    }
}
