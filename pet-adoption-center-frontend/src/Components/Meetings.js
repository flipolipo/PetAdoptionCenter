import React, { useState, useEffect, useContext } from 'react';
import { Link, useParams } from 'react-router-dom';
import { address_url } from '../Service/url';
import axios from 'axios';
import MyCalendar from '../Components/BigCalendarActivity/CalendarActivity';
import PetById from '../Pages/Pets/PetsById/PetById';
import './Meetings.css';
import { useUser } from './UserContext';

const Meetings = () => {
  const { userId } = useParams();
  const {user} = useUser();
  //console.log(userId);
  const [userData, setUserData] = useState({});

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${userId}`, {
          headers: {
            Authorization: `Bearer ${user.token}`,
          },
        });
        setUserData(response.data);
        console.log(response.data.Adoptions);
     
      } catch (err) {
        console.log(err);
      }
    };

    fetchProfileData();
  }, [user.id, user.token]);

  return (
    <div className="adoption-main-page-container">
      {userId && userData.Adoptions?.length >= 1 && (
        <>
          <h2 className="adoption-main-page">Your Adoptions</h2>
          {userData.Adoptions?.map((adoption) => (
            <div key={adoption.Id} className="adoption-card-meetings">
              <PetById petId={adoption.PetId} />
              <h3 className="adoption-main-page">
                Status:{' '}
                {adoption.IsContractAdoption ? 'Contracted' : 'Not Contracted'}
              </h3>
              {
                adoption.IsPreAdoptionPoll && !adoption.IsMeetings && (
                  <Link
                    to={`/Shelters/adoptions/${adoption.Id}/pets/${adoption.PetId}/users/${adoption.UserId}`}
                    className="adoption-main-page-link"
                  >
                    Choose meeting
                  </Link>
                ) /* : ((<Link
                to={`/Shelters/adoptions/${adoption.Id}/pets/${adoption.PetId}/users/${adoption.UserId}`}
                className="adoption-main-page-link"
              >
                Show more
              </Link>)) */
              }
              {adoption.Activity.Activities?.length >= 1 &&
                adoption.Activity.Activities.every(
                  (a) => new Date(a.EndActivityDate) < new Date()
                ) &&
                !adoption.IsMeetings &&
                !adoption.IsContractAdoption && (
                  <Link
                    to={`/Shelters/adoptions/${adoption.Id}/pets/${adoption.PetId}/users/${adoption.UserId}/confirm-adoption`}
                    className="adoption-main-page-link"
                  >
                    Confirm / delete your adoption
                  </Link>
                )}
              {adoption.IsMeetings && !adoption.IsContractAdoption && (
                <div className="basic-information-for-adoption">
                  <h2 className="important">
                    If you haven't filled in the basic information (first name,
                    last name, address), please proceed to your profile to
                    complete the details.
                  </h2>
                  {userId && (
                    <Link to={`/profile`} className="adoption-main-page-link">
                      Basic information
                    </Link>
                  )}{' '}
                </div>
              )}
              {adoption.IsMeetings && !adoption.IsContractAdoption && (
                <Link
                  to={`/Shelters/adoptions/${adoption.Id}/pets/${adoption.PetId}/users/${adoption.UserId}/contract-adoption`}
                  className="adoption-main-page-link"
                >
                  Contract adoption
                </Link>
              )}
             
            </div>
          ))}
           {userData.Adoptions?.map(
                (adoption) => (
                /*   console.log('IsMeetings:', adoption.IsMeetings),
                  console.log('IsContractAdoption:', adoption.IsContractAdoption), */
                  (!adoption.IsMeetings  && !adoption.IsContractAdoption && 
                  adoption.Activity.Activities?.length >= 1) ? (
                    <div key={adoption.Id}>
                      <h2>Adoption Meeting Calendar</h2>
                      <MyCalendar
                        events={adoption.Activity.Activities}
                        className="adoption-main-page-calendar"
                      />
                    </div>
                  ) : null
                )
              )}
        </>
      )}
    </div>
  );
};

export default Meetings;
