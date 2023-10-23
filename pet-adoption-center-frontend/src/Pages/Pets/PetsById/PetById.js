import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import MyCalendar from '../../../Components/BigCalendarActivity/CalendarActivity';
import { fetchCalendarDataForPet } from '../../../Service/fetchCalendarDataForPet';

const PetById = () => {
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
  const handleEventClick = (event) => {
    setSelectedActivity(event);
  }
  return (
    <div>
      <h1>Details for Pet with ID {id}</h1>
      <MyCalendar events={calendarData} onEventClick={handleEventClick}/>
    </div>
  );
};

export default PetById;
