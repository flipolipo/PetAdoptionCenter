import React, { useState } from "react";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";



const localizer = momentLocalizer(moment);

function MyCalendar({events, onEventClick}) {
  
  const formattedEvents = events.map((event) => ({
    id: event.Id,
    title: event.Name, 
    start: new Date(event.StartActivityDate), 
    end: new Date(event.EndActivityDate), 
  }));
  
  //console.log(formattedEvents);
  return (
    <div>
      
      <h1>Small Palls Calendar</h1>
      
      <Calendar
        localizer={localizer}
        events={formattedEvents} style={{height: 500, margin: "50px" }}
        startAccessor="start"
        endAccessor="end"
        onSelectEvent={onEventClick}
      />
      
     
    </div>
  );
}

export default MyCalendar;


