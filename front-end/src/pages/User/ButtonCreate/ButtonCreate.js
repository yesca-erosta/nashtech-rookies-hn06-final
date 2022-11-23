import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import styles from '../User.module.scss';

export const ButtonCreate = () => {
  const navigate = useNavigate();

  const navigateToCreateUser = () => {
    navigate('createuser');
  };

  return (
    <div className={styles.tableCreate}>
      <Button className={styles.btnCreate} onClick={navigateToCreateUser}>
        Create new user
      </Button>{' '}
    </div>
  );
};
