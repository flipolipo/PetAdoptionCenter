import React, { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import MyCalendar from '../../../Components/BigCalendarActivity/CalendarActivity';
import { fetchDataForPet } from '../../../Service/fetchDataForPet'
import { fetchCalendarDataForPet } from '../../../Service/fetchCalendarDataForPet'
import { DateTimePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import axios from 'axios';
import Modal from 'react-modal';
import './PetById.css';
import GenderPetLabel from '../../../Components/Enum/GenderPetLabel';
import SizePetLabel from '../../../Components/Enum/SizePetLabel';
import StatusPetLabel from '../../../Components/Enum/StatusPetLabel';
import { address_url } from '../../../Service/url';
import FlipCardAvailable from '../../../Components/FlipCardAvailable';

const PetById = ({ petData, setPetData, petId, userId, adoptionId }) => {
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
  const [shelterData, setShelterData] = useState([]);
  const [shelterAddress, setShelterAddress] = useState('');
  const [message, setMessage] = useState(null);
  const [choosenMeeting, setChoosenMeeting] = useState([]);
  const [petAdoptionData, setPetAdoptionData] = useState({});

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
    const fetchData = async (param) => {
      try {
        const calendarResponse = await fetchCalendarDataForPet(param);
        setCalendarData(calendarResponse.Activities);

        const dataResponse = await fetchDataForPet(param);
        setPetData(dataResponse);

        const shelterResponse = await axios.get(`${address_url}/Shelters/${dataResponse.ShelterId}`);
        setShelterData(shelterResponse.data);

        const shelterAddr = `${shelterResponse.data.ShelterAddress.City} ${shelterResponse.data.ShelterAddress.Street} ${shelterResponse.data.ShelterAddress.HouseNumber}/${shelterResponse.data.ShelterAddress.FlatNumber}`;
        setShelterAddress(shelterAddr);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    if (id) {
      fetchData(id);
    }
  }, [id]);

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

  const handleEventClick = (event) => {
    setSelectedActivity(event);
    setVisible(true);
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

      setCalendarData(resp.data);
      setMessage('Activity added successfully');
    } catch (err) {
      console.log(err);
      setMessage('Failed to add activity');
    }
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

      setCalendarData(resp.data);
      setMessage('Activity updated successfully');
    } catch (err) {
      console.log(err);
      setMessage('Failed to update activity');
    }
  };

  const removeActivity = async () => {
    try {
      const resp = await axios.delete(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities/${selectedActivity.id}`
      );

      setCalendarData(resp.data);
      setMessage('Activity deleted successfully');
    } catch (err) {
      console.log(err);
      setMessage('Failed to delete activity');
    }
  };

  const handleMeetForAdoption = async () => {
    try {
      const response = await axios.post(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${petId}/calendar/activities/${selectedActivity.id}/users/${userId}/adoptions/${adoptionId}/meetings-adoption`
      );

      setChoosenMeeting(response.data);
      setMessage('Meeting for adoption added successfully');
    } catch (error) {
      console.error(error);
      setMessage('Failed to add meeting for adoption');
    }
  };

  const goToPetCalendar = () => {
    setShowCalendar(true);
    setPetDataVisible(false);
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
                    <button className="pet-button">Adopt me virtually</button>
                    <button className="pet-button" onClick={goToPetCalendar}>
                      Take me for a walk
                    </button>
                    <button className="pet-button" onClick={goToPetCalendar}>
                      Make me visit at shelter
                    </button>
                  </>
                )}
              </div>
              <div className="pet-by-id-card-image-name">
                <img
                  className='pet-img'
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
                <button onClick={() => setEdit(true)}>edit</button>
                <button onClick={removeActivity}>Delete</button>
                <button onClick={handleMeetForAdoption}>Meet for adoption</button>
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
            <button onClick={handleSubmit}>Submit</button>
          </form>
        </LocalizationProvider>
      )}
    </div>
  );
};

export default PetById;
