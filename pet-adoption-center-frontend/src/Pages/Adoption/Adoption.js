import React, { useEffect, useState } from 'react';
import './Adoption.css';
import FlipCardAdopted from '../../Components/FlipCardAdopted';
import FlipCardAvailable from '../../Components/FlipCardAvailable';
import { Link } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';
import axios from 'axios';
import { address_url } from '../../Service/url';
import PreadoptionPoll from '../../Components/PreadoptionPoll';

const Adoption = ({ petData, setPetData }) => {
  const { user, setUser } = useUser();
  const [userData, setUserData] = useState([]);
  const [preadoptionPollVisible, setPreadoptionPollVisible] = useState(false);
  const [meetingsVisible, setMeettingsVisible] = useState(false);
  const [contractAdoptionVisible, setContractAdoptionVisible] = useState(false);

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${user.id}`, {
          headers: {
            Authorization: `Bearer ${user.token}`,
          },
        });
        setUserData(response.data);
        console.log(response.data);
      } catch (err) {
        console.log(err);
      }
    };

    fetchProfileData();
  }, [user.id, user.token]);

  console.log(user);
  console.log(petData);
  console.log(petData.ShelterId);

  const showPreadoptionPoll = () => {
    setPreadoptionPollVisible(true);
  };

  const hidePreadoptionPoll = () => {
    setPreadoptionPollVisible(false);
  };
  const showInfoMeetings = () => {
    setMeettingsVisible(true);
  };

  const hideInfoMeetings = () => {
    setMeettingsVisible(false);
  };
  const showContractAdoption = () => {
    setContractAdoptionVisible(true);
  };

  const hideContractAdoption = () => {
    setContractAdoptionVisible(false);
  };

  return (
    <div>
      <div className="adoption-container">
        {preadoptionPollVisible ? (
          <div className="preadoption-poll">
            {user.id && petData.Id ? (
              <PreadoptionPoll
                shelterId={petData.ShelterId}
                petId={petData.Id}
                userId={user.id}
              />
            ) : (
              <>
                <h2>Please log in and select a pet first</h2>
                <h3>PREADOPTION POLL</h3>
                <ul>
                  <li>Are you 18 years or over?*</li>
                  <li>
                    Are you willing to make the investment in both time and
                    finances to properly care for and manage your new pet?*
                  </li>
                  <li>Is there a fenced backyard?*</li>
                  <li>How many adults are in your household?*</li>
                  <li>Do ALL the members of your household want a new pet?*</li>
                  <li>Have you relinquished or given away any pets before?*</li>
                  <li>
                    Please list current pets residing at your home (including
                    roommates' pets also). Include pets' breed, age, sex,
                    spay/neuter status, number of years owned, and if they live
                    indoors or outdoors. *
                  </li>
                  <li>
                    In a 24-hour day, how long would the pet be left alone at a
                    given time?*
                  </li>
                  <li>Where will your new pet be kept when you are home?*</li>
                  <li>
                    Where will your new pet be kept when you are NOT at home?*
                  </li>
                  <li>
                    In a 24-hour day, how long would the animal be indoors?*
                  </li>
                  <li>Do you already have a veterinarian? If yes, Name:*</li>
                  <li>
                    What arrangements would you make for your new animal if you
                    went on a trip/vacation?*
                  </li>
                </ul>
              </>
            )}
            <button
              className="close-preadoption-poll"
              onClick={hidePreadoptionPoll}
            >
              Close Preadoption Poll
            </button>
          </div>
        ) : meetingsVisible ? (
          <div className="meetings">
             {preadoptionPollVisible ? (
       <></>
            ) : (
              <>
               
              </>
            )}
            <button onClick={hideInfoMeetings}>Close Info Meetings</button>
          </div>
        ) : contractAdoptionVisible ? (
          <div className="contract-adoption">
            <h2>SPRAWDZAM CZY TO DZIALA (Contract)</h2>
            <button onClick={hideContractAdoption}>
              Close Contract Adoption
            </button>
          </div>
        ) : (
          <>
            <div className="adoption-button-card">
              <div className="button-more-info">
                <button
                  className="button-adoption"
                  onClick={showPreadoptionPoll}
                >
                  Preadoption Poll
                </button>
                <button className="button-adoption" onClick={showInfoMeetings}>
                  Meetings to know your pet
                </button>
                <button
                  className="button-adoption"
                  onClick={showContractAdoption}
                >
                  Contract adoption
                </button>
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
