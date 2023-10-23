import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import MyCalendar from '../../../Components/BigCalendarActivity/CalendarActivity';
import { fetchCalendarDataForPet } from '../../../Service/fetchCalendarDataForPet';

const PetById = () => {
  const { id } = useParams();
  const [calendarData, setCalendarData] = useState([]);
  useEffect(() => {
    fetchCalendarDataForPet(id)
      .then((data) => {
        setCalendarData(data);
      })
      .catch((error) => console.error('Calendar download error:', error));
  }, [id]);
  return (
    <div>
      <h1>Details for Pet with ID {id}</h1>
      <MyCalendar events={calendarData}/>
    </div>
  );
};

export default PetById;
