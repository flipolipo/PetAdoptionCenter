import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useUser } from '../../Components/UserContext';
import { address_url } from '../../Service/url';
import UserRoleName from '../../Components/Enum/UserRoleName';
import MyCalendar from '../../Components/BigCalendarActivity/CalendarActivity';
import './Profile.css';

const Profile = () => {

    const [profileData, setProfileData] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const { user } = useUser();
    const [openSection, setOpenSection] = useState(null);
    const [isEditingBasicInfo, setIsEditingBasicInfo] = useState(false);
    const [editedBasicInfo, setEditedBasicInfo] = useState(null);

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
                setEditedBasicInfo(response.data.BasicInformation);
            } catch (err) {
                setError(err.message);
            } finally {
                setIsLoading(false);
            }
        };

        fetchProfileData();
    }, [user.id, user.token]);

    const updateUserProfile = async (id, updatedUserData) => {
        try {
            const response = await axios.put(`${address_url}/Users/${id}`, updatedUserData, {
                headers: {
                    'Authorization': `Bearer ${user.token}`
                }
            });

            if (response.status === 204) {
                console.log("User updated successfully");

            } else {
                console.error("Unexpected status code:", response.status);
            }

        } catch (error) {
            if (error.response) {
                switch (error.response.status) {
                    case 400:
                        console.error("Bad Request");
                        break;
                    case 404:
                        console.error("User not found");
                        break;
                    case 500:
                        console.error("Internal server error");
                        break;
                    default:
                        console.error("An error occurred:", error.message);
                }
            } else {
                console.error("Error sending request:", error.message);
            }
        }
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        if (name in editedBasicInfo) {
            setEditedBasicInfo(prev => ({ ...prev, [name]: value }));
        } else {
            setEditedBasicInfo(prev => ({
                ...prev,
                Address: { ...prev.Address, [name]: value }
            }));
        }
    };

    const startEditingBasicInfo = () => {
        setEditedBasicInfo(profileData.BasicInformation);
        setIsEditingBasicInfo(true);
    };

    const saveEditedBasicInfo = async () => {
        const updatedData = {
            ...profileData,
            BasicInformation: editedBasicInfo
        };

        await updateUserProfile(user.id, updatedData);
        setProfileData(updatedData);
        setIsEditingBasicInfo(false);
    };


    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;


    return (
        <div className="profile-container">
            <div className="headerProfile">
                <h1>Welcome, {profileData.BasicInformation?.Name}</h1>
            </div>
            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'BasicInformation' ? null : 'BasicInformation')}>
                    Basic Information <span className={openSection === 'BasicInformation' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'BasicInformation' && (
                    <div className="profile-details">
                        <h2>Basic Information</h2>
                        {isEditingBasicInfo ? (
                            <div>
                                <label>Name:
                                    <input type="text" name="Name" value={editedBasicInfo.Name} onChange={handleInputChange} />
                                </label>
                                <label>Surname:
                                    <input type="text" name="Surname" value={editedBasicInfo.Surname} onChange={handleInputChange} />
                                </label>
                                <label>Phone:
                                    <input type="text" name="Phone" value={editedBasicInfo.Phone} onChange={handleInputChange} />
                                </label>
                                <label>Street:
                                    <input type="text" name="Street" value={editedBasicInfo.Address.Street} onChange={handleInputChange} />
                                </label>
                                <label>City:
                                    <input type="text" name="City" value={editedBasicInfo.Address.City} onChange={handleInputChange} />
                                </label>
                                <button className="saveButton" onClick={saveEditedBasicInfo}>Save</button>
                            </div>
                        ) : (
                            <div key={profileData.BasicInformation.Id} className="profile-details-card">
                                <p>Name: {profileData.BasicInformation?.Name}</p>
                                <p>Surname: {profileData.BasicInformation?.Surname}</p>
                                <p>Phone: {profileData.BasicInformation?.Phone}</p>
                                <p>Address: {profileData.BasicInformation?.Address?.Street}, {profileData.BasicInformation?.Address?.City}</p>
                                <button className='editButton' onClick={startEditingBasicInfo}>Edit</button>
                            </div>
                        )}
                    </div>
                )}
            </div>
            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'Adoptions' ? null : 'Adoptions')}>
                    Adoptions <span className={openSection === 'Adoptions' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'Adoptions' && (
                    <div className="adoptionsProfiles">
                        <h2>Your Adoptions</h2>
                        {profileData.Adoptions?.map(adoption => (
                            <div key={adoption.Id} className="adoption-card">
                                <p>Pet ID: {adoption.PetId}</p>
                                <p>Status: {adoption.IsContractAdoption ? 'Contracted' : 'Not Contracted'}</p>
                                <p>Date of Adoption: {adoption.DateOfAdoption}</p>
                            </div>
                        ))}
                    </div>
                )}
            </div>
            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'Pets' ? null : 'Pets')}>
                    Pets <span className={openSection === 'Pets' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'Pets' && (
                    <div className="petsProfiles">
                        <h2>Your Pets</h2>
                        {profileData.Pets?.map(pet => (
                            <div key={pet.Id} className="pet-cardProfiles">
                                <p>Pet ID: {pet.Id}</p>
                                <p>Status: {pet.AvailbleForAdoption ? 'Availble for adoption' : 'Not availble for adoption'}</p>
                                <p>Discription: {pet.Description}</p>
                                <p><img
                                    src={`data:image/jpeg;base64, ${pet.Image}`}
                                    alt=""
                                    width="250px"
                                    height="100%"
                                /></p>
                            </div>
                        ))}
                    </div>
                )}
            </div>
            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'Roles' ? null : 'Roles')}>
                    Roles <span className={openSection === 'Roles' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'Roles' && (
                    <div className="rolesProfiles">
                        <h2>Your Roles</h2>
                        {profileData.Roles?.map(role => (
                            <div key={role.Id} className="role-card">
                                <p>Role ID: {role.Id}</p>
                                <p>Role title: {UserRoleName(role.Title)}</p>
                            </div>
                        ))}
                    </div>
                )}
            </div>
            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'Calendar' ? null : 'Calendar')}>
                    CalendarActivity <span className={openSection === 'Calendar' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'Calendar' && (
                    <div className="calendarProfiles">
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
                )}
            </div>


            <MyCalendar events={profileData.UserCalendar.Activities} />
            <></>
        </div>
    );
};

export default Profile;
