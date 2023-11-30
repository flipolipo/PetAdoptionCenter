import React, { useState, useEffect } from 'react';
import './TempHouseById.css';
import { Link, useParams } from 'react-router-dom';
import { FetchTempHouseDataForUser } from '../../Service/FetchTempHouseDataForUser';
import PetById from '../../Pages/Pets/PetsById/PetById';
import MyCalendar from '../BigCalendarActivity/CalendarActivity';

const TempHouseById = () => {
  const { tempHouseId, userId } = useParams();
  //console.log(userId);
  //console.log(tempHouseId);
  const [tempHouseData, setTempHouseData] = useState({});

  useEffect(() => {
    if (userId) {
      fetchDataTempHouse();
    }
  }, [userId]);

  const fetchDataTempHouse = async () => {
    if (userId) {
      try {
        const tempHouseResponseData = await FetchTempHouseDataForUser(userId);
        setTempHouseData(tempHouseResponseData.data);
       /*  console.log(tempHouseResponseData);
        console.log(tempHouseResponseData.PetsInTemporaryHouse);
        console.log(tempHouseResponseData.IsPreTempHousePoll);
        console.log(tempHouseResponseData.IsMeetings); */
      } catch (error) {
        console.error('Temporary house download error:', error);
      }
    }
  };
  return (
    <div className="temp-house-container">
      {userId && tempHouseData.PetsInTemporaryHouse?.length >= 1 && (
        <>
          <h2 className="temp-house-main-page">
            Your Pets In Temporary Housing
          </h2>
          {tempHouseData.PetsInTemporaryHouse?.map((pet) => (
            <div key={pet.Id} className="pet-in-temp-house">
              <PetById petsTempHouseId={pet.Id} tempHousesId={tempHouseData.Id} />
              <div className="button-container">
                {tempHouseData.IsPreTempHousePoll &&
                  !tempHouseData.IsMeetings && (
                    <Link
                      to={`/Shelters/temporaryHouses/${tempHouseData.Id}/pets/${pet.Id}/users/${userId}`}
                      className="temp-main-page-link-2"
                    >
                      Choose meeting
                    </Link>
                  )}
                {tempHouseData.Activity.Activities?.length >= 1 &&
                  tempHouseData.Activity.Activities.every((a) => new Date(a.EndActivityDate) < new Date() ) &&
                  !tempHouseData.IsMeetings && (
                    <Link
                      to={`/Shelters/temporaryHouses/${tempHouseData.Id}/pets/${pet.Id}/confirm-delete`}
                      className="temp-main-page-link-2"
                    >
                      Confirm / delete your temporary house
                    </Link>
                  )}
              </div>
            </div>
          ))}
          {!tempHouseData.IsMeetings &&
          tempHouseData.Activity.Activities?.length >= 1 ? (
            <>
              <h2>Temporary House Meeting Calendar</h2>
              <MyCalendar
                events={tempHouseData.Activity.Activities}
                className="temp-house-calendar"
              />
            </>
          ) : null}
        </>
      )}
    </div>
  );
  
};

export default TempHouseById;
