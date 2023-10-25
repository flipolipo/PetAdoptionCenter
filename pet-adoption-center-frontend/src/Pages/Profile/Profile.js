import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useUser } from '../../Components/UserContext';
import { address_url } from '../../Service/url';
import UserRoleName from '../../Components/Enum/UserRoleName';
import MyCalendar from '../../Components/BigCalendarActivity/CalendarActivity';
const Profile = () => {

    const [profileData, setProfileData] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const { user } = useUser();


    useEffect(() => {
        const fetchProfileData = async () => {
            try {
                const response = await axios.get(`${address_url}/Users/${user.id}`, {
                    headers: {
                        'Authorization': `Bearer ${user.token}`
                    }
                });
                console.log(response)
                setProfileData(response.data);
            } catch (err) {
                setError(err.message);
            } finally {
                setIsLoading(false);
            }
        };

        fetchProfileData();
    }, [user.id, user.token]);

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;


    return (
        <div className="profile-container">
            <div className="header">
                <h1>Welcome, {profileData.BasicInformation?.Name}</h1>
            </div>
            <div className="profile-details">
                <h2>Basic Information</h2>
                <p>Name: {profileData.BasicInformation?.Name}</p>
                <p>Surname: {profileData.BasicInformation?.Surname}</p>
                <p>Phone: {profileData.BasicInformation?.Phone}</p>
                <p>Address: {profileData.BasicInformation?.Address?.Street}, {profileData.BasicInformation?.Address?.City}</p>
            </div>
            <div className="adoptions">
                <h2>Your Adoptions</h2>
                {profileData.Adoptions?.map(adoption => (
                    <div key={adoption.Id} className="adoption-card">
                        <p>Pet ID: {adoption.PetId}</p>
                        <p>Status: {adoption.IsContractAdoption ? 'Contracted' : 'Not Contracted'}</p>
                        <p>Date of Adoption: {adoption.DateOfAdoption}</p>
                    </div>
                ))}
            </div>
            <div className="pets">
                <h2>Your Pets</h2>
                {profileData.Pets?.map(pet => (
                    <div key={pet.Id} className="pet-card">
                        <p>Pet ID: {pet.Id}</p>
                        <p>Status: {pet.AvailbleForAdoption ? 'Availble for adoption' : 'Not availble for adoption'}</p>
                        <p>Discription: {pet.Description}</p>
                        <p>{pet.ImageBase64}</p>
                    </div>
                ))}
            </div>
            <div className="roles">
                <h2>Your Roles</h2>
                {profileData.Roles?.map(role => (
                    <div key={role.Id} className="role-card">
                        <p>Role ID: {role.Id}</p>
                        <p>Role title: {UserRoleName(role.Title)}</p>
                    </div>
                ))}
            </div>
            <div className="calendar">
                <h2>Your Calendar</h2>
                <p>Calendar Id: {profileData.UserCalendar.Id}</p>
                {profileData.UserCalendar.Activities?.map(activity => (
                    <div key={activity.Id} className="calendar-card">
                        <p>Activity ID: {activity.Id}</p>
                        <p>Activity Name: {activity.Name}</p>
                        <p>Activity Start: {activity.StartActivityDate}</p>
                        <p>Activity End: {activity.EndActivityDate}</p>
                    </div>
                ))}
            </div>
            <MyCalendar events={profileData.UserCalendar.Activities} />
        </div>
    );
};

export default Profile;
