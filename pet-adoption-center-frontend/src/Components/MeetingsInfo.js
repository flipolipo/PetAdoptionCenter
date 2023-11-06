import React from 'react';
import './MeetingsInfo.css';

const MeetingsInfo = () => {
  return (
    <div className="meetingsInfo-container">
      <h2 className="meetingsInfo-h2">
        Please LOG IN, CHOOSE your new best friend and first SIGN the
        pre-adoption QUESTIONNAIRE
      </h2>
      <h3 className="meetingsInfo-h3">MEETINGS TO KNOW YOUR PET</h3>
      <p className="meetingsInfo-container-p">
        To proceed to the next adoption step, you must SELECT AT LEAST ONE
        MEETINGS with your chosen pet to get better acquainted. The meeting
        should be chosen from the pet's available calendar. When you have gotten
        to know your pet (completed all the meetings added to the adoption
        calendar), a button will appear for CONFIRMING that you are still
        interested.
      </p>
    </div>
  );
};

export default MeetingsInfo;
