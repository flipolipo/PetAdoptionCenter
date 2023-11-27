import React, { useState, useEffect } from 'react';
import './TemporaryHouse.css';
import FlipCardAvailable from '../../Components/FlipCardAvailable';
import { Link, useParams } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';
import { FetchTempHouseDataForUser } from '../../Service/FetchTempHouseDataForUser';

const TemporaryHouse = () => {
  const { shelterId, petId } = useParams();
  /*  console.log(petId);
  console.log(shelterId); */
  const { user, setUser } = useUser();
  //console.log(user);
  const [tempHouseData, setTempHouseData] = useState({});

  useEffect(() => {
    if (user.id) {
      fetchDataTempHouse();
    }
  }, [user.id]);

  const fetchDataTempHouse = async () => {
    if (user.id) {
      try {
        const tempHouseResponseData = await FetchTempHouseDataForUser(user.id);
        setTempHouseData(tempHouseResponseData.data);
        /*   console.log(tempHouseResponseData);
        console.log(tempHouseResponseData.PetsInTemporaryHouse);
        console.log(tempHouseResponseData.IsPreTempHousePoll);
        console.log(tempHouseResponseData.IsMeetings); */
      } catch (error) {
        console.error('Temporary house download error:', error);
      }
    }
  };

  //console.log(tempHouseData);
  return (
    <div className="temporary-house-container">
      <div className="temporary-house-button-card">
        <div className="button-more-info">
          {(petId || tempHouseData?.IsPreTempHousePoll) ? null : (
            <Link to={`/Users/pets`} className="find-pet">
              Find your new best friend
            </Link>
          )}
          {user.id && petId && !tempHouseData?.IsPreTempHousePoll ? (
            <Link
              to={`/Shelters/${shelterId}/temporaryHouses/pets/${petId}/users/${user.id}/pre-temporary-house-poll`}
              className="find-pet"
            >
              Pre-Temporary House Poll
            </Link>
          ) : (
            !tempHouseData?.IsPreTempHousePoll && (
              <Link
                to={`/Shelters/temporaryHouses/pets/users/pre-temporary-house-poll-info`}
                className="find-pet"
              >
                Pre-Temporary House Poll
              </Link>
            )
          )}
          {user.id && tempHouseData ? (
            <Link
              to={`/Shelters/temporaryHouses/${tempHouseData?.Id}/pets/users/${user.id}`}
              className="find-pet"
            >
              Meetings to know your pet
            </Link>
          ) : (
            !tempHouseData?.IsMeetings && (
              <Link
                to={`/Shelters/temporaryHouses/pets/users/meetings-temporary-house-poll-info`}
                className="find-pet"
              >
                Meetings to know your pet
              </Link>
            )
          )}
        </div>
        <div className="temporary-house-card-page">
          <div className="temporary-house-info">
            {petId && !user.id ? (
              <h2 className="important">
                Please first to start temporary house process sign up or sign in
                and remember to fill your personal details (name, surname,
                address)? This is necessary to complete the pre temporary house
                poll.
              </h2>
            ) : null}
            <h2 className="title-pet">TEMPORARY HOUSING PROCCESS FOR PETS</h2>
            <ul className="temporary-house-process">
              <li>
                Sign up on our website and complete the basic information on
                your profile.
              </li>
              <li>Log into your account.</li>
              <li>Select a pet from the available options.</li>
              <li>
                Please be prepared to schedule one meeting per week with the pet
                in case someone is interested in adoption.
              </li>
              <li>Complete the pre-temporary house poll for the chosen pet.</li>
              <li>
                Choose at least one meeting from the calendar named "Know me."
              </li>
              <li>
                After the meeting, you will be asked to confirm that you are
                still interested to give a temporary home for your chosen pet.
                If you change your mind, you can choose to delete the proccess
                for temporary house.
              </li>
              <li>
                You will receive a message that you can come to us to submit
                your personal signature (remember to bring your document) and
                pick up your new friend.
              </li>
            </ul>
            <h3 className="important">
              Feel free to click on the bottom to have more information
            </h3>
          </div>
        </div>
      </div>
      <div className="petsAvailableForAdoption">
        <div className="pet-inscription">
          <h2 className="title-pet">Pets available for adoption</h2>
        </div>
        <div className="pet-card">
          <FlipCardAvailable />
        </div>
      </div>
    </div>
  );
};
export default TemporaryHouse;
