import React from 'react';
import './home.scss';

import { ModalFirstChangePassword } from './Modal/ModalFirstChangePassword';

function Home() {
    return (
        <>
            <ModalFirstChangePassword />

            <h1>Home </h1>
        </>
    );
}

export default Home;
