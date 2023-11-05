import React, { useEffect, useState } from 'react';
import './Adoption.css';
import FlipCardAdopted from '../../Components/FlipCardAdopted';
import FlipCardAvailable from '../../Components/FlipCardAvailable';
import { Link, useParams } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';
import axios from 'axios';
import { address_url } from '../../Service/url';
import PreadoptionPollInfo from '../../Components/PreadoptionPollInfo';
import MeetingsInfo from '../../Components/MeetingsInfo';
import ContractAdoptionInfo from '../../Components/ContractAdoptionInfo';

const Adoption = () => {
  const { id } = useParams();
  //console.log(id);
  const { user, setUser } = useUser();
  const [userData, setUserData] = useState([]);
  const [preadoptionPollVisible, setPreadoptionPollVisible] = useState(false);
  const [meetingsVisible, setMeettingsVisible] = useState(false);
  const [contractAdoptionVisible, setContractAdoptionVisible] = useState(false);
  const [calendarAdoptionVisible, setCalendarAdoptionVisible] = useState(false);

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${user.id}`, {
          headers: {
            Authorization: `Bearer ${user.token}`,
          },
        });
        setUserData(response.data);
        // console.log(response.data.Adoptions);
      } catch (err) {
        console.log(err);
      }
    };

    fetchProfileData();
  }, [user.id, user.token]);

  const showPreadoptionPoll = async () => {
    setPreadoptionPollVisible(true);
  };

  const hidePreadoptionPoll = async () => {
    setPreadoptionPollVisible(false);
  };
  const showInfoMeetings = async () => {
    setMeettingsVisible(true);
  };

  const hideInfoMeetings = () => {
    setMeettingsVisible(false);
  };
  const showContractAdoption = async () => {
    setContractAdoptionVisible(true);
  };

  const hideContractAdoption = async () => {
    setContractAdoptionVisible(false);
  };

  return (
    <div>
      <div className="adoption-container">
        {preadoptionPollVisible ? (
          <div className="button-more-info-1">
            <PreadoptionPollInfo />
            <button className="button-adoption-1" onClick={hidePreadoptionPoll}>
              Close Preadoption Poll
            </button>
          </div>
        ) : meetingsVisible ? (
          <div className="meetings">
            <MeetingsInfo />
            <button onClick={hideInfoMeetings}>Close Info Meetings</button>
          </div>
        ) : contractAdoptionVisible ? (
          <div className="contract-adoption">
            <ContractAdoptionInfo />
            <button onClick={hideContractAdoption}>
              Close Contract Adoption
            </button>
          </div>
        ) : (
          <>
            <div className="adoption-button-card">
              <div className="button-more-info">
                {user.id && id ? (
                  <Link
                    to={`/Shelters/adoptions/pets/${id}/users/${user.id}`}
                    className="find-pet"
                  >
                    Preadoption Poll
                  </Link>
                ) : (
                  <div className="button-more-info">
                    <button
                      className="button-adoption"
                      onClick={showPreadoptionPoll}
                    >
                      Preadoption Poll
                    </button>
                  </div>
                )}
                {user.id ? (
                  <Link
                    to={`/Shelters/adoptions/pets/users/${user.id}`}
                    className="find-pet"
                  >
                    Meetings to know your pet
                  </Link>
                ) : (
                  <div className="button-more-info">
                    <button
                      className="button-adoption"
                      onClick={showInfoMeetings}
                    >
                      Meetings to know your pet
                    </button>
                  </div>
                )}
                {user.id ? (
                  <Link
                    to={`/Shelters/adoptions/pets/users/${user.id}`}
                    className="find-pet"
                  >
                    Contract adoption
                  </Link>
                ) : (
                  <div className="button-more-info">
                    <button
                      className="button-adoption"
                      onClick={showContractAdoption}
                    >
                      Contract adoption
                    </button>
                  </div>
                )}
                <Link to={`/Users/pets`} className="find-pet">
                  Find your new best friend
                </Link>
              </div>
              <div className="adoption-card-page-adoption">
                <div className="pet-inscription">
                  <h2 className="title-pet">Adopted pets</h2>
                </div>
                <FlipCardAdopted />
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
          </>
        )}
      </div>
    </div>
  );
};

export default Adoption;
