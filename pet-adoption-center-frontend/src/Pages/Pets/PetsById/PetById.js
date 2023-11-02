import React, { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import MyCalendar from '../../../Components/BigCalendarActivity/CalendarActivity';
import { fetchCalendarDataForPet } from '../../../Service/fetchCalendarDataForPet';
import { fetchDataForPet } from '../../../Service/fetchDataForPet';
import { DateTimePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { address_url } from '../../../Service/url';
import axios from 'axios';
import Modal from 'react-modal';
import './PetById.css';
import GenderPetLabel from '../../../Components/Enum/GenderPetLabel';
import SizePetLabel from '../../../Components/Enum/SizePetLabel';
import StatusPetLabel from '../../../Components/Enum/StatusPetLabel';
import FlipCardAvailable from '../../../Components/FlipCardAvailable';
import { fetchDataForShelter } from '../../../Service/fetchDataForShelter';

const PetById = ({ petId, userId, adoptionId }) => {
  const { id } = useParams();
  const [calendarData, setCalendarData] = useState([]);
  const [selectedActivity, setSelectedActivity] = useState(null);
  const [startDate, setStartDate] = useState(Date.now());
  const [endDate, setEndDate] = useState(Date.now());
  const [activityName, setActivityName] = useState('');
  const [visible, setVisible] = useState(false);
  const [edit, setEdit] = useState(false);
  const [showCalendar, setShowCalendar] = useState(false);
  const [petDataVisible, setPetDataVisible] = useState(true);
  const [message, setMessage] = useState(null);
  const [choosenMeeting, setChoosenMeeting] = useState([]);
  const [petData, setPetData] = useState({});
  const [shelterData, setShelterData] = useState({});
  const [shelterAddress, setShelterAddress] = useState('');

  Modal.setAppElement('#root');

  const customStyles = {
    content: {
      top: '50%',
      left: '50%',
      right: 'auto',
      bottom: 'auto',
      marginRight: '-50%',
      transform: 'translate(-50%, -50%)',
    },
  };
  useEffect(() => {
    if (id) {
      fetchData(id);
    }
  }, [id]);

  useEffect(() => {
    if (petId) {
      fetchData(petId);
    }
  }, [petId]);

  const fetchData = async (param) => {
    try {
      const calendarData = await fetchCalendarDataForPet(param);
      setCalendarData(calendarData.Activities);

      const petDataById = await fetchDataForPet(param);
      setPetData(petDataById);
      console.log(petDataById);

      if (petDataById && petDataById.ShelterId) {
        const shelterDataById = await fetchDataForShelter(
          petDataById.ShelterId
        );
        setShelterData(shelterDataById);
        console.log(shelterDataById);
        console.log(shelterDataById.Name);

        if (shelterDataById) {
          setShelterAddress(
            `${shelterDataById.ShelterAddress.City} ${shelterDataById.ShelterAddress.Street} ${shelterDataById.ShelterAddress.HouseNumber}/${shelterDataById.ShelterAddress.FlatNumber}`
          );
        }
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const updateEndDate = (e) => {
    const date = new Date(e);
    const iso = date.toISOString();
    setEndDate(iso);
  };
  const updateStartDate = (e) => {
    const date = new Date(e);
    const iso = date.toISOString();
    setStartDate(iso);
  };
  const handleSubmit = async () => {
    try {
      const resp = await axios.post(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities`,
        {
          Name: activityName,
          StartActivityDate: startDate,
          EndActivityDate: endDate,
        }
      );
      /*      console.log('activityName: ' + activityName);
      console.log('activityStart: ' + startDate);
      console.log('activityEnd: ' + endDate);
      console.log('Pet :' + petData);
      console.log('response: ' + resp.data); */
      setCalendarData(resp.data);
    } catch (err) {
      console.log(err);
    }
  };
  const handleEventClick = (event) => {
    setSelectedActivity(event);
    //console.log(event);
    setVisible(true);
  };
  const goToPetCalendar = () => {
    setShowCalendar(true);
    setPetDataVisible(false);
  };
  const updateActivity = async () => {
    try {
      const resp = await axios.put(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities/${selectedActivity.id}`,
        {
          Name: activityName,
          StartActivityDate: startDate,
          EndActivityDate: endDate,
        }
      );
      //console.log(resp);
    } catch (err) {
      console.log(err);
    }
  };
  const RemoveActivity = async () => {
    try {
      const resp = await axios.delete(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities/${selectedActivity.id}`
      );
      console.log(resp);
    } catch (err) {
      console.log(err);
    }
  };
  const handleMeetForAdoption = async () => {
    try {
      const response = await axios.post(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${petId}/calendar/activities/${selectedActivity.id}/users/${userId}/adoptions/${adoptionId}/meetings-adoption`
      );
      // console.log(response);
      setChoosenMeeting(response.data);
      setMessage('Meeting for adoption added successfully');
    } catch (error) {
      console.error(error);
      setMessage('Failed to add meeting for adoption');
    }
  };

  return (
    <div>
      {petData && petData.BasicHealthInfo && petDataVisible ? (
        <>
          <div className="pet-by-id-container">
            <div className="img-and-info">
              <div className="botton-pet-by-id">
                {petData.AvaibleForAdoption && (
                  <>
                    <Link to="/Shelters/adoptions">
                      <button className="pet-button">Adopt Me</button>
                    </Link>
                    <Link to="/Shelters/temporaryHouses">
                      <button className="pet-button">
                        Give me a temporary house
                      </button>
                    </Link>
                  </>
                )}
                {petData.Status !== 3 && (
                  <>
                    {' '}
                    <button className="pet-button">Adopt me virtually</button>
                    <button className="pet-button" onClick={goToPetCalendar}>
                      Take me for a walk
                    </button>
                    <button className="pet-button" onClick={goToPetCalendar}>
                      Make me visit at shelter
                    </button>
                    <button
                      className="go-to-calendar"
                      onClick={goToPetCalendar}
                    >
                      My calendar
                    </button>
                  </>
                )}
              </div>
              <div className="pet-by-id-card-image-name">
                <img
                  className="pet-img"
                  src={`data:image/jpeg;base64, ${petData.ImageBase64}`}
                  alt=""
                />
                <img
                  src={process.env.PUBLIC_URL + '/Photo/whitePaw.png'}
                  alt="Lapka"
                  className="paw-icon"
                />
                <h2>{petData.BasicHealthInfo.Name}</h2>
              </div>
              <div className="more-info-pet-by-id">
                <h3>Age: {petData.BasicHealthInfo.Age}</h3>
                <h3>Size: {SizePetLabel(petData.BasicHealthInfo.Size)}</h3>
                <h3>Gender: {GenderPetLabel(petData.Gender)}</h3>
                <h3>Status: {StatusPetLabel(petData.Status)}</h3>
                {shelterData && shelterData.Name && (<h3>Shelter name: {shelterData.Name}</h3>)}
                <h3>Shelter Address: {shelterAddress}</h3>
                <h3>
                  Is available for adoption:{' '}
                  {petData.AvaibleForAdoption ? 'Yes' : 'No'}
                </h3>
              </div>
            </div>
            <div className="description-pet-by-id">
              <h2>Description: {petData.Description}</h2>
            </div>
          </div>

          <div className="pets-available-to-adoption">
            <div className="pet-inscription">
              <h2>Pets available for adoption</h2>
            </div>
            <div className="pet-card">
              <FlipCardAvailable />
            </div>
          </div>
        </>
      ) : null}
      {petId && petDataVisible ? (
        <div className="meetings-button-pet-adoption">
          <button className="go-to-calendar" onClick={goToPetCalendar}>
            Know your pet
          </button>
        </div>
      ) : null}
      {showCalendar && (
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <Modal
            isOpen={visible}
            onRequestClose={() => setVisible(false)}
            style={customStyles}
          >
            {edit ? (
              <div className="modal-content">
                Title:{' '}
                <input
                  onChange={(e) => setActivityName(e.target.value)}
                  type="text"
                ></input>
                Start Date:{' '}
                <DateTimePicker onChange={(e) => updateStartDate(e)} />
                End Date: <DateTimePicker onChange={(e) => updateEndDate(e)} />
                <button onClick={updateActivity}>done</button>
                <button onClick={() => setEdit(false)}>go back</button>
              </div>
            ) : (
              <div>
                <button onClick={() => setEdit(true)}>Edit</button>
                <button onClick={() => RemoveActivity}>Delete</button>
                <button onClick={handleMeetForAdoption}>
                  Meet for adoption
                </button>
              </div>
            )}
          </Modal>
          <h1>Details for Pet with ID {id}</h1>
          <MyCalendar events={calendarData} onEventClick={handleEventClick} />
          <form>
            Name:{' '}
            <input
              type="text"
              onChange={(e) => setActivityName(e.target.value)}
            ></input>
            Start Date: <DateTimePicker onChange={(e) => updateStartDate(e)} />
            End Date: <DateTimePicker onChange={(e) => updateEndDate(e)} />
            <button onClick={handleSubmit}>asdasd</button>
          </form>
        </LocalizationProvider>
      )}
    </div>
  );
};

export default PetById;
