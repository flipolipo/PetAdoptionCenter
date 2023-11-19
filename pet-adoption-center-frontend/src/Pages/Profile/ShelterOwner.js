import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './ShelterOwner.css';
import { address_url } from '../../Service/url';
import { useParams } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';
import MyCalendar from '../../Components/BigCalendarActivity/CalendarActivity';
import GenericCard from '../../Components/GenericCard';
import Modal from 'react-modal';

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
    const [petsAvailable, setPetsAvailable] = useState([]);
    const [imageFile, setImageFile] = useState(null);
    const [visible, setVisible] = useState(false);
    const [vacinationVisible, setVacinationVisible] = useState(false)
    const [diseaseVisible, setDiseaseVisible] = useState(false)
    const [activityAddVisible, setActivityAddVisible] = useState(false)
    const [activityFormData, setActivityFormData] = useState({
        name: "",
        startActivityDate: new Date().toISOString(),
        endActivityDate: new Date().toISOString()
    })

    const [vaccinationForm, setVaccinationForm] = useState({
        vaccinationName: '',
        date: new Date().toISOString()
    });

    const [diseaseForm, setDiseaseForm] = useState({
        nameOfdisease: '',
        illnessStart: new Date().toISOString(),
        illnessEnd: new Date().toISOString()
    });

    const [formData, setFormData] = useState({
        Type: '',
        Gender: '',
        BasicHealthInfo: {
            Name: '',
            Age: '',
            Size: '',
            IsNeutered: false,
        },
        Description: '',
        Status: '',
        AvaibleForAdoption: false,
        ImageFile: null,
    });


    const handleVacinationInputChange = (e) => {
        const { name, value } = e.target;

        setVaccinationForm(prev => ({ ...prev, [name]: value }));
        console.log(vaccinationForm)
    };

    const handleDiseaseInputChange = (e) => {
        const { name, value } = e.target;

        setDiseaseForm(prev => ({ ...prev, [name]: value }));
        console.log(diseaseForm)
    };

    const handleActivityInputChange = (e) => {
        const { name, value } = e.target;

        setActivityFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;

        if (name.startsWith('BasicHealthInfo.')) {
            const nestedProperty = name.split('.')[1];
            setFormData({
                ...formData,
                BasicHealthInfo: {
                    ...formData.BasicHealthInfo,
                    [nestedProperty]: type === 'checkbox' ? checked : value,
                },
            });
        } else {
            setFormData({
                ...formData,
                [name]: type === 'checkbox' ? checked : value,
            });
        }

    };

    const handleAddActivitySubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post(`${address_url}/Shelters/${shelterId}/calendar/activities`,
                {
                    name: activityFormData.name,
                    startActivityDate: activityFormData.startActivityDate,
                    endActivityDate: activityFormData.endActivityDate
                }, {
                params: {
                    shelterId: shelterId
                }
            },
                {
                    headers: {
                        Authorization: `Bearer ${user.token}`,
                        'Content-Type': 'multipart/form-data',
                    },
                }
            )
            if (response.status === 200) {

                console.log('Activity added successfully');
                console.log(response)
                setActivityFormData({
                    name: '',
                    startActivityDate: new Date().toISOString(),
                    endActivityDate: new Date().toISOString()
                })

            } else {
                console.error('Failed to add activity. Status:', response.status);
            }
        } catch (error) {
            console.error('Failed to add activity:', error);
        }
    }



    const handleSubmit = async (e) => {
        e.preventDefault();


        try {
            const response = await axios.post(
                `${address_url}/Shelters/${shelterId}/pets`,
                formData,
                {
                    headers: {
                        Authorization: `Bearer ${user.token}`,
                        'Content-Type': 'multipart/form-data',
                    },
                }
            );

            if (response.status === 201) {

                console.log('Pet added successfully');

                setImageFile(null);

            } else {
                console.error('Failed to add pet. Status:', response.status);
            }
        } catch (error) {
            console.error('Failed to add pet:', error);
        }

    };

    const handleAddVacination = async (shelterId, petId) => {
        console.log(shelterId)
        console.log(petId)
        console.log(vaccinationForm)
        try {
            const response = await axios.post(
                `${address_url}/Shelters/${shelterId}/pets/${petId}/vaccinations`,
                {
                    vaccinationName: vaccinationForm.vaccinationName,
                    date: vaccinationForm.date
                },
                {
                    headers: {
                        Authorization: `Bearer ${user.token}`,
                        'Content-Type': 'application/json-patch+json',
                    }
                }
            );

            if (response.status === 201) {
                console.log('Vaccination added successfully');
                setVaccinationForm({ vaccinationName: '', date: new Date().toISOString() });
            } else {
                console.error(`Failed to add vaccination. Status: ${response.status}`);
            }
        } catch (error) {
            if (error.response) {
                console.error('Error adding vaccination:', error.response.data.message);
            } else if (error.request) {
                console.error('No response received:', error.request);
            } else {
                console.error('Error:', error.message);
            }
        }
    };

    const handleAddDisease = async (shelterId, petId) => {
        try {
            const response = await axios.post(
                `${address_url}/Shelters/${shelterId}/pets/${petId}/diseases`,
                {

                    nameOfdisease: diseaseForm.nameOfdisease,
                    illnessStart: diseaseForm.illnessStart,
                    illnessEnd: diseaseForm.illnessEnd
                },
                {
                    headers: {
                        Authorization: `Bearer ${user.token}`,
                        'Content-Type': 'application/json-patch+json',
                    }
                }
            );

            if (response.status === 201) {
                console.log('Disease added successfully');
                setDiseaseForm({ nameOfdisease: '', illnessStart: new Date().toISOString(), illnessEnd: new Date().toISOString() });
            } else {
                console.error(`Failed to add disease. Status: ${response.status}`);
            }
        } catch (error) {
            if (error.response) {
                console.error('Error adding disease:', error.response.data.message);
            } else if (error.request) {
                console.error('No response received:', error.request);
            } else {
                console.error('Error:', error.message);
            }
        }
    };

    function deletePetHandler(shelterId, petId) {
        const url = `${address_url}/Shelters/${shelterId}/pets/${petId}`;
        console.log(petId);

        axios
            .delete(url)
            .then((response) => {
                if (response.status === 204) {
                    console.log('Pet deleted successfully');
                    setPetsAvailable((currentPets) =>
                        currentPets.filter((pet) => pet.Id !== petId)
                    );
                }
            })
            .catch((error) => {
                if (error.response) {
                    if (error.response.status === 404) {
                        console.error('Pet not found');
                    } else {
                        console.error('Error deleting pet:', error.message);
                    }
                } else if (error.request) {
                    console.error('No response received:', error.request);
                } else {
                    console.error('Error:', error.message);
                }
            });
    }

    useEffect(() => {
        GetAvailablePetsForAdoption();
    }, []);


    async function GetAvailablePetsForAdoption() {
        try {
            const response = await axios.get(
                `${address_url}/Shelters/${shelterId}/pets`
            );
            console.log(response.data);
            setPetsAvailable(response.data);
        } catch (error) {
            console.error(error);
        }
    }

    const handleAddUser = async (e) => {
        e.preventDefault();
        console.log(role);
        console.log(userId);
        try {
            const response = await axios.post(
                `${address_url}/Shelters/${shelterId}/users`,
                {
                    title: role,
                },
                {
                    params: {
                        userId: userId,
                    },
                },
                {
                    headers: {
                        Authorization: `Bearer ${user.token}`,
                        'Content-Type': 'application/json',
                    },
                }
            );

            if (response.status === 200) {
                console.log('User succesfuly added');
            } else {
                console.error(
                    'Failed to add user to shelter. Status:',
                    response.status
                );
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
                        phone: editedPhone,
                    },
                    headers: {
                        Authorization: `Bearer ${user.token}`,
                    },
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
                const profileDataPromises = shelterUsers.map(async (user) => {
                    const response = await axios.get(`${address_url}/Users/${user.Id}`, {
                        headers: {
                            Authorization: `Bearer ${user.token}`,
                        },
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
                        Authorization: `Bearer ${user.token}`,
                    },
                });

                setProfileData(response.data);
            } catch (err) {
                console.error(err);
            }
        };

        fetchProfileData();
    }, [user.id, user.token]);

    useEffect(() => {
        axios
            .get(`${address_url}/shelters/${shelterId}`)
            .then((response) => {
                setShelterDetails(response.data);
                setShelterUsers(response.data.ShelterUsers);
                setShelterPets(response.data.ShelterPets);
                setShelterActivities(response.data.ShelterCalendar.Activities);
                setShelterAdoptions(response.data.Adoptions);
            })
            .catch((error) => console.error(error));
    }, [shelterId]);

    return (
        <div className="shelter-owner-container">
            <h1 className="shelter-title">{shelterDetails.Name}</h1>

            <div className="section">
                <div
                    className="section-header"
                    onClick={() =>
                        setOpenSection(
                            openSection === 'ShelterInformation' ? null : 'ShelterInformation'
                        )
                    }
                >
                    Shelter Information{' '}
                    <span
                        className={
                            openSection === 'ShelterInformation'
                                ? 'arrow-down'
                                : 'arrow-right'
                        }
                    >
                        ➤
                    </span>
                </div>

                {openSection === 'ShelterInformation' && (
                    <div className="shelter-info">
                        <h2 className="shelter-info-title">Informations</h2>

                        <div className="shelter-info-content">
                            <div className="shelter-basic-info">
                                <p className="shelter-name">
                                    <strong>Name:</strong> {shelterDetails.Name}
                                </p>
                                <p className="shelter-phone">
                                    <strong>Phone Number:</strong> {shelterDetails.PhoneNumber}
                                </p>
                                {profileData && (
                                    <p className="shelter-owner">
                                        <strong>Shelter Owner:</strong>{' '}
                                        {profileData.BasicInformation.Name}{' '}
                                        {profileData.BasicInformation.Surname}{' '}
                                        <strong>Phone:</strong>
                                        {profileData.BasicInformation.Phone} <strong>Email:</strong>{' '}
                                        {profileData.Email}
                                    </p>
                                )}
                                {shelterDetails.ShelterAddress && (
                                    <div className="shelter-address">
                                        <strong>Address:</strong>
                                        <ul>
                                            {shelterDetails.ShelterAddress.HouseNumber && (
                                                <p>
                                                    <strong>House Number:</strong>{' '}
                                                    {shelterDetails.ShelterAddress.HouseNumber}
                                                </p>
                                            )}
                                            {shelterDetails.ShelterAddress.Street && (
                                                <p>
                                                    <strong>Street:</strong>{' '}
                                                    {shelterDetails.ShelterAddress.Street}
                                                </p>
                                            )}
                                            {shelterDetails.ShelterAddress.FlatNumber && (
                                                <p>
                                                    <strong>Flat Number:</strong>{' '}
                                                    {shelterDetails.ShelterAddress.FlatNumber}
                                                </p>
                                            )}
                                            {shelterDetails.ShelterAddress.City && (
                                                <p>
                                                    <strong>City:</strong>{' '}
                                                    {shelterDetails.ShelterAddress.City}
                                                </p>
                                            )}
                                            {shelterDetails.ShelterAddress.PostalCode && (
                                                <p>
                                                    <strong>Postal Code:</strong>{' '}
                                                    {shelterDetails.ShelterAddress.PostalCode}
                                                </p>
                                            )}
                                        </ul>
                                    </div>
                                )}
                            </div>
                            {shelterDetails.ImageBase64 && (
                                <div className="shelter-image-container">
                                    <img
                                        src={`data:image/jpeg;base64, ${shelterDetails.ImageBase64}`}
                                        alt="Shelter"
                                        className="shelter-image"
                                        width="250px"
                                        height="100%"
                                    />
                                </div>
                            )}

                            {editMode ? (
                                <form onSubmit={handleUpdateShelter}>
                                    <input
                                        type="text"
                                        value={editedName}
                                        onChange={(e) => setEditedName(e.target.value)}
                                    />
                                    <textarea
                                        value={editedDescription}
                                        onChange={(e) => setEditedDescription(e.target.value)}
                                    />
                                    <input
                                        type="text"
                                        value={editedStreet}
                                        onChange={(e) => setEditedStreet(e.target.value)}
                                    />
                                    <input
                                        type="text"
                                        value={editedHouseNumber}
                                        onChange={(e) => setEditedHouseNumber(e.target.value)}
                                    />
                                    <input
                                        type="text"
                                        value={editedPostalCode}
                                        onChange={(e) => setEditedPostalCode(e.target.value)}
                                    />
                                    <input
                                        type="text"
                                        value={editedCity}
                                        onChange={(e) => setEditedCity(e.target.value)}
                                    />
                                    <input
                                        type="text"
                                        value={editedPhone}
                                        onChange={(e) => setEditedPhone(e.target.value)}
                                    />
                                    <button type="submit">Save Changes</button>
                                    <button type="button" onClick={() => setEditMode(false)}>
                                        Cancel
                                    </button>
                                </form>
                            ) : (
                                <button onClick={() => setEditMode(true)}>Edit Shelter</button>
                            )}
                        </div>
                        <div className="shelter-description">
                            <p>
                                <strong>Description:</strong>
                            </p>
                            <p>{shelterDetails.ShelterDescription}</p>
                        </div>
                    </div>
                )}
            </div>

            <div className="section">
                <div
                    className="section-header"
                    onClick={() =>
                        setOpenSection(
                            openSection === 'ShelterUsers' ? null : 'ShelterUsers'
                        )
                    }
                >
                    Shelter Users{' '}
                    <span
                        className={
                            openSection === 'ShelterUsers' ? 'arrow-down' : 'arrow-right'
                        }
                    >
                        ➤
                    </span>
                </div>
                {openSection === 'ShelterUsers' && (
                    <div className="shelter-users">
                        <h2 className="shelter-users-title">Shelter Users:</h2>
                        <ul className="shelter-users-list">
                            {usersProfileData.map((userData) => (
                                <li className="shelter-user-item" key={userData.Id}>
                                    <p>
                                        <strong>Name:</strong> {userData.BasicInformation.Name}
                                    </p>
                                    <p>
                                        <strong>Surname:</strong>{' '}
                                        {userData.BasicInformation.Surname}
                                    </p>
                                    <p>
                                        <strong>Phone:</strong> {userData.BasicInformation.Phone}
                                    </p>
                                    <p>
                                        <strong>Email:</strong> {userData.Email}
                                    </p>
                                </li>
                            ))}
                        </ul>

                        <form>
                            <label>
                                User ID:
                                <input
                                    type="text"
                                    value={userId}
                                    onChange={(e) => setUserId(e.target.value)}
                                />
                            </label>
                            <label>
                                Role:
                                <input
                                    type="text"
                                    value={role}
                                    onChange={(e) => setRole(e.target.value)}
                                />
                            </label>
                            <button type="submit" onClick={handleAddUser}>
                                Add User to Shelter
                            </button>
                        </form>
                    </div>
                )}
            </div>

            <div className="section">
                <div
                    className="section-header"
                    onClick={() =>
                        setOpenSection(openSection === 'ShelterPets' ? null : 'ShelterPets')
                    }
                >
                    Shelter Pets{' '}
                    <span
                        className={
                            openSection === 'ShelterPets' ? 'arrow-down' : 'arrow-right'
                        }
                    >
                        ➤
                    </span>
                </div>
                {openSection === 'ShelterPets' && (
                    <div className="shelter-pets">
                        <h2 className="shelter-pets-title">Shelter {shelterId} Pets:</h2>
                        <>
                            <button className="buttonSignIn" onClick={() => { setVisible(true); }}>Add Pet</button>
                            <Modal isOpen={visible} onRequestClose={() => setVisible(false)} >
                                <div className="modal-shelter-pets-content">
                                    <form onSubmit={handleSubmit}>
                                        <label>
                                            Type:
                                            <input
                                                type="number"
                                                name="Type"
                                                value={formData.Type}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Gender:
                                            <input
                                                type="number"
                                                name="Gender"
                                                value={formData.Gender}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Name:
                                            <input
                                                type="text"
                                                name="BasicHealthInfo.Name"
                                                value={formData.BasicHealthInfo.Name}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Age:
                                            <input
                                                type="number"
                                                name="BasicHealthInfo.Age"
                                                value={formData.BasicHealthInfo.Age}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Size:
                                            <input
                                                type="number"
                                                name="BasicHealthInfo.Size"
                                                value={formData.BasicHealthInfo.Size}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Is Neutered:
                                            <input
                                                type="checkbox"
                                                name="BasicHealthInfo.IsNeutered"
                                                checked={formData.BasicHealthInfo.IsNeutered}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Description:
                                            <textarea
                                                name="Description"
                                                value={formData.Description}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Status:
                                            <input
                                                type="number"
                                                name="Status"
                                                value={formData.Status}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Available For Adoption:
                                            <input
                                                type="checkbox"
                                                name="AvaibleForAdoption"
                                                checked={formData.AvaibleForAdoption}
                                                onChange={handleInputChange}
                                            />
                                        </label>

                                        <label>
                                            Image File:
                                            <input
                                                type="file"
                                                name="ImageFile"
                                                onChange={(e) => setFormData({ ...formData, ImageFile: e.target.files[0] })}
                                            />
                                        </label>

                                        <button type="submit">Submit</button>
                                    </form>

                                    <button className="buttonPetAddBack" onClick={() => setVisible(false)}>Back</button>
                                </div>
                            </Modal>
                        </>
                        <ul className="shelter-pets-list">
                            {petsAvailable.map((pet, index) => (
                                <p>
                                    <GenericCard
                                        key={index}
                                        className="generic--pet-card-shelter-owner"
                                        pet={pet}
                                    />{' '}
                                    <button
                                        onClick={() => deletePetHandler(shelterId, pet.Id)}
                                        className="delete-pet"
                                    >
                                        Delete
                                    </button>

                                    <button className="buttonAddMedicalHistory" onClick={() => { setDiseaseVisible(true); }}>Add medical history</button>
                                    <Modal isOpen={diseaseVisible} onRequestClose={() => setDiseaseVisible(false)} >
                                        <div className="modal-shelter-pets-add-vacination">
                                            <form>
                                                <label>
                                                    Name:
                                                    <input
                                                        type="text"
                                                        name="nameOfdisease"
                                                        value={diseaseForm.nameOfdiseaseameOfdisease}
                                                        onChange={handleDiseaseInputChange}
                                                    />
                                                </label>
                                                <label>
                                                    Start Date:
                                                    <input
                                                        type="datetime-local"
                                                        name="illnessStart"
                                                        value={diseaseForm.illnessStart.slice(0, -5)}
                                                        onChange={(e) => setDiseaseForm({ ...diseaseForm, illnessStart: e.target.value + ":00.000Z" })}
                                                    />
                                                </label>
                                                <label>
                                                    End Date:
                                                    <input
                                                        type="datetime-local"
                                                        name="illnessEnd"
                                                        value={diseaseForm.illnessEnd.slice(0, -5)}
                                                        onChange={(e) => setDiseaseForm({ ...diseaseForm, illnessEnd: e.target.value + ":00.000Z" })}
                                                    />
                                                </label>
                                            </form>
                                            <button onClick={() => handleAddDisease(shelterId, pet.Id)} className='addDisease'>Add Disease</button>
                                            <button className="buttonDiseaseBack" onClick={() => setDiseaseVisible(false)}>Back</button>
                                        </div>
                                    </Modal>
                                    <button className="buttonAddVacination" onClick={() => { setVacinationVisible(true); }}>Add vacination</button>
                                    <Modal isOpen={vacinationVisible} onRequestClose={() => setVacinationVisible(false)} >
                                        <div className="modal-shelter-pets-add-vacination">
                                            <form>
                                                <label>
                                                    Name:
                                                    <input
                                                        type="text"
                                                        name="vaccinationName"
                                                        value={vaccinationForm.vaccinationName}
                                                        onChange={handleVacinationInputChange}
                                                    />
                                                </label>
                                                <label>
                                                    Date:
                                                    <input
                                                        type="datetime-local"
                                                        name="date"
                                                        value={vaccinationForm.date.slice(0, -5)}
                                                        onChange={(e) => setVaccinationForm({ ...vaccinationForm, date: e.target.value + ":00.000Z" })}
                                                    />
                                                </label>

                                            </form>
                                            <button onClick={() => handleAddVacination(shelterId, pet.Id)} className='addVaccination'>Add Vaccination</button>
                                            <button className="buttonVacinationBack" onClick={() => setVacinationVisible(false)}>Back</button>
                                        </div>
                                    </Modal>
                                </p>
                            ))}
                        </ul>
                    </div>
                )}
            </div>

            <div className="section">
                <div
                    className="section-header"
                    onClick={() =>
                        setOpenSection(
                            openSection === 'ShelterAdoptions' ? null : 'ShelterAdoptions'
                        )
                    }
                >
                    Shelter Adoptions{' '}
                    <span
                        className={
                            openSection === 'ShelterAdoptions' ? 'arrow-down' : 'arrow-right'
                        }
                    >
                        ➤
                    </span>
                </div>
                {openSection === 'ShelterAdoptions' && (
                    <div className="shelter-adoptions">
                        <h2 className="shelter-adoptions-title">Shelter Adoptions:</h2>
                        <ul className="shelter-adoptions-list">
                            {shelterAdoptions.map((adoption) => (
                                <div className="adoption-info" key={adoption.Id}>
                                    <h2 className="adoption-info-title">Adoption Information</h2>
                                    <div className="adoption-general-info">
                                        <h3>General Info:</h3>
                                        <p>
                                            <strong>Adoption ID:</strong> {adoption.Id}
                                        </p>
                                        <p>
                                            <strong>Date of Adoption:</strong>{' '}
                                            {adoption.DateOfAdoption}
                                        </p>
                                        <p>
                                            <strong>Pet ID:</strong> {adoption.PetId}
                                        </p>
                                        <p>
                                            <strong>User ID:</strong> {adoption.UserId}
                                        </p>
                                    </div>

                                    <div className="adoption-status">
                                        <h3>Status:</h3>
                                        <p>
                                            <strong>Contract Adoption:</strong>{' '}
                                            {adoption.IsContractAdoption ? 'Completed' : 'Pending'}
                                        </p>
                                        <p>
                                            <strong>Meetings:</strong>{' '}
                                            {adoption.IsMeetings ? 'Held' : 'Not Held'}
                                        </p>
                                        <p>
                                            <strong>Pre-Adoption Poll:</strong>{' '}
                                            {adoption.IsPreAdoptionPoll ? 'Filled' : 'Not Filled'}
                                        </p>
                                    </div>

                                    {adoption.PreadoptionPoll && (
                                        <p className="adoption-poll-info">
                                            <strong>Poll Info:</strong> {adoption.PreadoptionPoll}
                                        </p>
                                    )}
                                    {adoption.Comments && (
                                        <p className="adoption-comments">
                                            <strong>Comments:</strong> {adoption.Comments}
                                        </p>
                                    )}
                                </div>
                            ))}
                        </ul>
                    </div>
                )}
            </div>
            <div className='shelter-calendar-activites-shelter-owner'>

                <MyCalendar events={shelterActivities} />
                <button className="buttonAddActivity" onClick={() => { setActivityAddVisible(true); }}>Add Activity</button>
                <Modal isOpen={activityAddVisible} onRequestClose={() => setActivityAddVisible(false)} >
                    <form onSubmit={handleAddActivitySubmit}>
                        <label>
                            Name:
                            <input
                                type="text"
                                name="name"
                                value={activityFormData.name}
                                onChange={handleActivityInputChange}
                            />
                        </label>
                        <label>
                            Start Date:
                            <input
                                type="datetime-local"
                                name="startActivityDate"
                                value={activityFormData.startActivityDate.slice(0, -5)}
                                onChange={(e) => setActivityFormData({ ...activityFormData, startActivityDate: e.target.value + ":00.000Z" })}
                            />
                        </label>
                        <label>
                            End Date:
                            <input
                                type="datetime-local"
                                name="endActivityDate"
                                value={activityFormData.endActivityDate.slice(0, -5)}
                                onChange={(e) => setActivityFormData({ ...activityFormData, endActivityDate: e.target.value + ":00.000Z" })}
                            />
                        </label>
                    </form>
                    <button className='button-add-activity-shelterowner' onClick={handleAddActivitySubmit}>Add Activity</button>
                    <button className="buttonShelterActivityBack" onClick={() => setActivityAddVisible(false)}>Back</button>
                </Modal>

            </div>

        </div >
    );
};

export default ShelterOwner;
