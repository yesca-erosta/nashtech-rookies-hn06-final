import cssStyles from './Loading.module.scss';

export const Loading = () => {
    return (
        <section className={cssStyles.loading}>
            <div className={cssStyles.spinner}>
                <span>loading...</span>
            </div>
        </section>
    );
};
