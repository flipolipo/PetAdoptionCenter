import React from "react";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";

const localizer = momentLocalizer(moment);
const events = [
  {
    title: "Spotkanie z klientem",
    start: new Date(),
    end: new Date(),
  },
]
function MyCalendar() {
  return (
    <div>
      <h1>Small Palls Calendar</h1>
      <Calendar
        localizer={localizer}
        events={events} style={{height: 500, margin: "50px" }}
        startAccessor="start"
        endAccessor="end"
      />
    </div>
  );
}

export default MyCalendar;


