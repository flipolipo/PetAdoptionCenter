import React from 'react';
import './MeetingsTempHouseInfo.css';

const MeetingsTempHouseInfo = () => {
  return (
    <div className="meetingsInfo-container">
      <div className="go-to-home-adoption">
        <h2 className="important">
          First SIGN the pre-temporary-house QUESTIONNAIRE
        </h2>
      </div>
      <h3 className="meetingsInfo-h3">MEETINGS TO KNOW YOUR PET</h3>
      <p className="meetingsInfo-container-p">
        To proceed to the next step in the temporary housing process for your
        chosen pet, you must SELECT AT LEAST ONE MEETING with the pet to get
        better acquainted. The meeting should be chosen from the pet's available
        calendar. Once you've completed all the scheduled meetings added to the
        adoption calendar, a button will appear for CONFIRMATION, indicating
        that you are still interested. If you've changed your mind, you will
        also have the option to delete the temporary housing process.
      </p>
    </div>
  );
};

export default MeetingsTempHouseInfo;
