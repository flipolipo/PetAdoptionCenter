// Pages/Profile.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useUser } from '../../Components/UserContext';
import { address_url } from '../../Service/url';

const Profile = () => {
    const { user, setUser } = useUser();
    return (
        <div>
            <h2>Welcome, {user.username}!</h2>

            <div>
                <h3>Your Adoptions:</h3>
                <ul>

                </ul>


            </div>

            <div>
                <h3>Edit Profile</h3>


            </div>
        </div>
    );
};

export default Profile;
