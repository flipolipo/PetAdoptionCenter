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

const PetById = ({ petData, setPetData }) => {
  const { id } = useParams();
  const [calendarData, setCalendarData] = useState([]);
  const [selectedActivity, setSelectedActivity] = useState(null);
  const [startDate, setStartDate] = useState(Date.now());
  const [endDate, setEndDate] = useState(Date.now());
  const [activityName, setActivityName] = useState('');
  const [visible, setVisible] = useState(false);
  const [edit, setEdit] = useState(false);
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
    fetchCalendarDataForPet(id)
      .then((data) => {
        setCalendarData(data.Activities);
        console.log(data.Activities);
      })
      .catch((error) => console.error('Calendar download error:', error));
  }, [id]);

  useEffect(() => {
    fetchDataForPet(id)
      .then((data) => {
        setPetData(data);
        //console.log(data);
      })
      .catch((error) => console.error('Calendar download error:', error));
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
      console.log('activityName: ' + activityName);
      console.log('activityStart: ' + startDate);
      console.log('activityEnd: ' + endDate);
      console.log('Pet :' + petData);
      console.log('response: ' + resp.data);
    } catch (err) {
      console.log(err);
    }
  };
  const handleEventClick = (event) => {
    setSelectedActivity(event);
    console.log(event);
    setVisible(true);
  };
  const updateActivity = async () => {
    try {
      const resp = await axios.put(
        `https://localhost:7292/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities/${selectedActivity.id}`,
        {
          Name: activityName,
          StartActivityDate: startDate,
          EndActivityDate: endDate,
        }
      );
      console.log(resp);
    } catch (err) {
      console.log(err);
    }
    
  };
  const RemoveActivity = async () => {
    try {
      const resp = await axios.delete(
        `https://localhost:7292/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities/${selectedActivity.id}`
      );
      console.log(resp);
    } catch (err) {
      console.log(err);
    }
  };
  return (
    <div>
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
              <button onClick={() => setEdit(false)}>Go back</button>
            </div>
          ) : (
            <div>
              <button onClick={() => setEdit(true)}>Edit</button>
              <input type='button' value='Delete' onClick={RemoveActivity}></input>
            </div>
          )}
        </Modal>
        <h1>Details for Pet with ID {id}</h1>
        <MyCalendar
          events={calendarData}
          onEventClick={handleEventClick}
          // onSelectEvent={(slotInfo) => onSelectEventHandler(slotInfo)}
        />

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

        <Link to="/Shelters/adoptions">
          <button className="go-to-adoption">Adopt Me</button>
        </Link>
      </LocalizationProvider>
    </div>
  );
};

export default PetById;
