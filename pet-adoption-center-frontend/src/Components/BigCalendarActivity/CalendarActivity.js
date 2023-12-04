import React, { useState } from 'react';
import { Calendar, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';

const localizer = momentLocalizer(moment);

function MyCalendar({ events, onEventClick }) {
  const formattedEvents = events.map((event) => ({
    id: event.Id,
    title: event.Name,
    start: new Date(event.StartActivityDate),
    end: new Date(event.EndActivityDate),
  }));

  return (
    <div>
      <Calendar
        localizer={localizer}
        events={formattedEvents}
        style={{ height: 500, margin: '50px' }}
        startAccessor="start"
        endAccessor="end"
        onSelectEvent={onEventClick}
      />
    </div>
  );
}

export default MyCalendar;
