import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

function User() {
    let navigate = useNavigate();

    const navigateToCreateUser = () => {
        navigate('createuser');
    };

    const navigateToEditUser = () => {
        navigate('edituser');
    };

    return (
        <div>
            <h1>User </h1>
            <Button onClick={navigateToCreateUser}>Create new user</Button>
            <hr></hr>
            <Button onClick={navigateToEditUser}>Edit user ..............</Button>
        </div>
    );
}

export default User;
