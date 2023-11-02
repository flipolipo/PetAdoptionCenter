import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './ShelterOwner.css';
import { address_url } from '../../Service/url';
import { useParams } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';
import MyCalendar from '../../Components/BigCalendarActivity/CalendarActivity';


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
    const [editMode, setEditMode] = useState(false);
    const [editedName, setEditedName] = useState('');
    const [editedDescription, setEditedDescription] = useState('');
    const [editedStreet, setEditedStreet] = useState('');
    const [editedHouseNumber, setEditedHouseNumber] = useState('');
    const [editedPostalCode, setEditedPostalCode] = useState('');
    const [editedCity, setEditedCity] = useState('');
    const [editedPhone, setEditedPhone] = useState('');
    const [userId, setUserId] = useState('');
    const [role, setRole] = useState('');

    const handleAddUser = async (e) => {
        e.preventDefault();
        console.log(role)
        console.log(userId)
        try {
            const response = await axios.post(
                `${address_url}/Shelters/${shelterId}/users`,
                {
                    title: role
                }, {
                params:
                {
                    userId: userId
                }
            },
                {
                    headers: {
                        'Authorization': `Bearer ${user.token}`,
                        'Content-Type': 'application/json',
                    },
                }
            );

            if (response.status === 200) {
                console.log("User succesfuly added")
            } else {

                console.error('Failed to add user to shelter. Status:', response.status);
            }
        } catch (error) {
            console.error('Failed to add user to shelter:', error);
        }
    };



    useEffect(() => {

        if (shelterDetails) {
            setEditedName(shelterDetails.Name || '');
            setEditedDescription(shelterDetails.ShelterDescription || '');
            setEditedStreet(shelterDetails.ShelterAddress?.Street || '');
            setEditedHouseNumber(shelterDetails.ShelterAddress?.HouseNumber || '');
            setEditedPostalCode(shelterDetails.ShelterAddress?.PostalCode || '');
            setEditedCity(shelterDetails.ShelterAddress?.City || '');
            setEditedPhone(shelterDetails.PhoneNumber || '');
        }
    }, [shelterDetails]);

    const handleUpdateShelter = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.put(
                `${address_url}/Shelters/${shelterId}`,
                null,
                {
                    params: {
                        name: editedName,
                        description: editedDescription,
                        street: editedStreet,
                        houseNumber: editedHouseNumber,
                        postalCode: editedPostalCode,
                        city: editedCity,
                        phone: editedPhone
                    },
                    headers: {
                        'Authorization': `Bearer ${user.token}`
                    }
                }
            );

            if (response.status === 200) {

                const updatedShelter = response.data;
                setShelterDetails(updatedShelter);
                setEditMode(false);
            } else {

            }
        } catch (error) {
            console.error('Failed to update the shelter:', error);
        }
    };



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
                            {shelterDetails.ImageBase64 && (<div className="shelter-image-container">
                                <img
                                    src={`data:image/jpeg;base64, ${shelterDetails.ImageBase64}`}
                                    alt="Shelter"
                                    className="shelter-image"
                                    width="250px"
                                    height="100%"
                                />
                            </div>)}

                            {editMode ? (
                                <form onSubmit={handleUpdateShelter}>
                                    <input type="text" value={editedName} onChange={(e) => setEditedName(e.target.value)} />
                                    <textarea value={editedDescription} onChange={(e) => setEditedDescription(e.target.value)} />
                                    <input type="text" value={editedStreet} onChange={(e) => setEditedStreet(e.target.value)} />
                                    <input type="text" value={editedHouseNumber} onChange={(e) => setEditedHouseNumber(e.target.value)} />
                                    <input type="text" value={editedPostalCode} onChange={(e) => setEditedPostalCode(e.target.value)} />
                                    <input type="text" value={editedCity} onChange={(e) => setEditedCity(e.target.value)} />
                                    <input type="text" value={editedPhone} onChange={(e) => setEditedPhone(e.target.value)} />
                                    <button type="submit">Save Changes</button>
                                    <button type="button" onClick={() => setEditMode(false)}>Cancel</button>
                                </form>
                            ) : (
                                <button onClick={() => setEditMode(true)}>Edit Shelter</button>
                            )}
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
                                <li className="shelter-user-item" key={userData.Id}>
                                    <p><strong>Name:</strong> {userData.BasicInformation.Name}</p>
                                    <p><strong>Surname:</strong> {userData.BasicInformation.Surname}</p>
                                    <p><strong>Phone:</strong> {userData.BasicInformation.Phone}</p>
                                    <p><strong>Email:</strong> {userData.Email}</p>
                                </li>
                            ))}
                        </ul>

                        {/* User Addition Form */}
                        <form >
                            <label>
                                User ID:
                                <input type="text" value={userId} onChange={(e) => setUserId(e.target.value)} />
                            </label>
                            <label>
                                Role:
                                <input type="text" value={role} onChange={(e) => setRole(e.target.value)} />
                            </label>
                            <button type="submit" onClick={handleAddUser}>Add User to Shelter</button>
                        </form>
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

            <MyCalendar events={shelterActivities} />
        </div>
    );


};

export default ShelterOwner;
