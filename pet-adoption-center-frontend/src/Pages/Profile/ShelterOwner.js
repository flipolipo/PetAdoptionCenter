import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './ShelterOwner.css';
import { address_url } from '../../Service/url';
import { useParams } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';


const ShelterOwner = () => {
    const { shelterId } = useParams();
    const [shelterDetails, setShelterDetails] = useState({});
    const [shelterUsers, setShelterUsers] = useState([]);
    const [shelterPets, setShelterPets] = useState([]);
    const [shelterActivities, setShelterActivities] = useState([]);
    const [shelterAdoptions, setShelterAdoptions] = useState([]);
    const { user } = useUser();
    const [profileData, setProfileData] = useState(null);
    const [openSection, setOpenSection] = useState(null);
    const [usersProfileData, setUsersProfileData] = useState([]);

    useEffect(() => {

        const fetchUsersProfileData = async () => {
            try {

                const profileDataPromises = shelterUsers.map(async user => {
                    const response = await axios.get(`${address_url}/Users/${user.Id}`, {
                        headers: {
                            'Authorization': `Bearer ${user.token}`
                        }
                    });
                    return response.data;
                });


                const allProfileData = await Promise.all(profileDataPromises);


                setUsersProfileData(allProfileData);



            } catch (err) {
                console.error(err);
            }
        };


        fetchUsersProfileData();
    }, [shelterUsers]);

    useEffect(() => {
        const fetchProfileData = async () => {
            try {
                const response = await axios.get(`${address_url}/Users/${user.id}`, {
                    headers: {
                        'Authorization': `Bearer ${user.token}`
                    }
                });

                setProfileData(response.data);

            } catch (err) {
                console.error(err)
            }
        };

        fetchProfileData();
    }, [user.id, user.token]);

    useEffect(() => {
        axios.get(`${address_url}/shelters/${shelterId}`)
            .then(response => {
                setShelterDetails(response.data);
                setShelterUsers(response.data.ShelterUsers);
                setShelterPets(response.data.ShelterPets);
                setShelterActivities(response.data.ShelterCalendar.Activities);
                setShelterAdoptions(response.data.Adoptions);

            })
            .catch(error => console.error(error));
    }, [shelterId]);

    return (
        <div className="shelter-owner-container">
            <h1 className="shelter-title">{shelterDetails.Name}</h1>

            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'ShelterInformation' ? null : 'ShelterInformation')}>
                    Shelter Information <span className={openSection === 'ShelterInformation' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>

                {openSection === 'ShelterInformation' && (
                    <div className="shelter-info">
                        <h2 className="shelter-info-title">Informations</h2>

                        <div className="shelter-info-content">
                            <div className="shelter-basic-info">
                                <p className="shelter-name"><strong>Name:</strong> {shelterDetails.Name}</p>
                                <p className="shelter-phone"><strong>Phone Number:</strong> {shelterDetails.PhoneNumber}</p>
                                {profileData && (
                                    <p className="shelter-owner">
                                        <strong>Shelter Owner:</strong> {profileData.BasicInformation.Name} {profileData.BasicInformation.Surname} <strong>Phone:</strong>{profileData.BasicInformation.Phone} <strong>Email:</strong> {profileData.Email}
                                    </p>
                                )}
                                {shelterDetails.ShelterAddress && (
                                    <div className="shelter-address">
                                        <strong>Address:</strong>
                                        <ul>
                                            {shelterDetails.ShelterAddress.HouseNumber && <p><strong>House Number:</strong> {shelterDetails.ShelterAddress.HouseNumber}</p>}
                                            {shelterDetails.ShelterAddress.Street && <p><strong>Street:</strong> {shelterDetails.ShelterAddress.Street}</p>}
                                            {shelterDetails.ShelterAddress.FlatNumber && <p><strong>Flat Number:</strong> {shelterDetails.ShelterAddress.FlatNumber}</p>}
                                            {shelterDetails.ShelterAddress.City && <p><strong>City:</strong> {shelterDetails.ShelterAddress.City}</p>}
                                            {shelterDetails.ShelterAddress.PostalCode && <p><strong>Postal Code:</strong> {shelterDetails.ShelterAddress.PostalCode}</p>}
                                        </ul>
                                    </div>
                                )}
                            </div>

                            <div className="shelter-image-container">
                                <img
                                    src={`data:image/jpeg;base64, ${shelterDetails.ImageBase64}`}
                                    alt="Shelter"
                                    className="shelter-image"
                                    width="250px"
                                    height="100%"
                                />
                            </div>
                        </div>
                        <div className="shelter-description">
                            <p ><strong>Description:</strong></p>
                            <p>{shelterDetails.ShelterDescription}</p>
                        </div>
                    </div>
                )}
            </div>

            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'ShelterUsers' ? null : 'ShelterUsers')}>
                    Shelter Users <span className={openSection === 'ShelterUsers' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'ShelterUsers' && (
                    <div className="shelter-users">

                        <h2 className="shelter-users-title">Shelter Users:</h2>
                        <ul className="shelter-users-list">
                            {usersProfileData.map(userData => (
                                <li className="shelter-user-item">
                                    <p><strong>Name:</strong> {userData.BasicInformation.Name}</p>
                                    <p><strong>Surname:</strong> {userData.BasicInformation.Surname}</p>
                                    <p><strong>Phone:</strong> {userData.BasicInformation.Phone}</p>
                                    <p><strong>Email:</strong> {userData.Email}</p>

                                </li>
                            ))}
                        </ul>
                    </div>
                )}
            </div>

            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'ShelterPets' ? null : 'ShelterPets')}>
                    Shelter Pets <span className={openSection === 'ShelterPets' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'ShelterPets' && (
                    <div className="shelter-pets">

                        <h2 className="shelter-pets-title">Shelter Pets:</h2>
                        <ul className="shelter-pets-list">
                            {shelterPets.map(pet => (
                                <li className="shelter-pet-item" key={pet.Id}>
                                    <p><strong>Pet Name:</strong> {pet.BasicHealthInfo.Name}</p>
                                    <p><strong>Pet Age:</strong> {pet.BasicHealthInfo.Age}</p>
                                    <p><strong>Pet Age:</strong> {console.log(pet)}</p>
                                </li>
                            ))}
                        </ul>
                    </div>
                )}
            </div>

            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'ShelterActivities' ? null : 'ShelterActivities')}>
                    Shelter Activities <span className={openSection === 'ShelterActivities' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'ShelterActivities' && (
                    <div className="shelter-activities">

                        <h2 className="shelter-activities-title">Shelter Activities:</h2>
                        <ul className="shelter-activities-list">
                            {shelterActivities.map(activity => (
                                <li className="shelter-activity-item" key={activity.Id}>
                                    <p><strong>Activity Name:</strong> {activity.Name}</p>
                                </li>
                            ))}
                        </ul>
                    </div>
                )}
            </div>

            <div className="section">
                <div className="section-header" onClick={() => setOpenSection(openSection === 'ShelterAdoptions' ? null : 'ShelterAdoptions')}>
                    Shelter Adoptions <span className={openSection === 'ShelterAdoptions' ? 'arrow-down' : 'arrow-right'}>➤</span>
                </div>
                {openSection === 'ShelterAdoptions' && (
                    <div className="shelter-adoptions">

                        <h2 className="shelter-adoptions-title">Shelter Adoptions:</h2>
                        <ul className="shelter-adoptions-list">
                            {shelterAdoptions.map(adoption => (
                                <div className="adoption-info" key={adoption.Id}>
                                    <h2 className="adoption-info-title">Adoption Information</h2>
                                    <div className="adoption-general-info">
                                        <h3>General Info:</h3>
                                        <p><strong>Adoption ID:</strong> {adoption.Id}</p>
                                        <p><strong>Date of Adoption:</strong> {adoption.DateOfAdoption}</p>
                                        <p><strong>Pet ID:</strong> {adoption.PetId}</p>
                                        <p><strong>User ID:</strong> {adoption.UserId}</p>
                                    </div>

                                    <div className="adoption-status">
                                        <h3>Status:</h3>
                                        <p><strong>Contract Adoption:</strong> {adoption.IsContractAdoption ? "Completed" : "Pending"}</p>
                                        <p><strong>Meetings:</strong> {adoption.IsMeetings ? "Held" : "Not Held"}</p>
                                        <p><strong>Pre-Adoption Poll:</strong> {adoption.IsPreAdoptionPoll ? "Filled" : "Not Filled"}</p>
                                    </div>

                                    {adoption.PreadoptionPoll && <p className="adoption-poll-info"><strong>Poll Info:</strong> {adoption.PreadoptionPoll}</p>}
                                    {adoption.Comments && <p className="adoption-comments"><strong>Comments:</strong> {adoption.Comments}</p>}
                                </div>
                            ))}
                        </ul>
                    </div>
                )}
            </div>
        </div>
    );


};

export default ShelterOwner;
