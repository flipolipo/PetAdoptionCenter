import React, { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import MyCalendar from '../../../Components/BigCalendarActivity/CalendarActivity';
import { fetchCalendarDataForPet } from '../../../Service/fetchCalendarDataForPet';
import { fetchDataForPet } from '../../../Service/fetchDataForPet';
import Adoption from '../../Adoption/Adoption.js'

const PetById = ({petData, setPetData}) => {
  const { id } = useParams();
  const [calendarData, setCalendarData] = useState([]);
  const [selectedActivity, setSelectedActivity] = useState(null);
  

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
        console.log(data);
      })
      .catch((error) => console.error('Calendar download error:', error));
  }, [id]);

  const handleEventClick = (event) => {
    setSelectedActivity(event);
  }
  return (
    <div>
      <h1>Details for Pet with ID {id}</h1>
      <MyCalendar events={calendarData} onEventClick={handleEventClick}/>
      <Link to="/Shelters/adoptions">
        <button className='go-to-adoption'>Adopt Me</button>
      </Link>
    </div>
  );
};

export default PetById;
