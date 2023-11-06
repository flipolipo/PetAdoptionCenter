import React, { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import { address_url } from '../Service/url';
import axios from 'axios';
import { FetchDataForAdoption } from '../Service/FetchDataForAdoption';
import './ConfirmAdoption.css';

const ConfirmAdoption = () => {
  const { adoptionId, petId, userId } = useParams();
  console.log(petId);
  console.log(userId);
  console.log(adoptionId);
  const [adoptionData, setAdoptionData] = useState({});
  const [confirmAdoption, setConfirmAdoption] = useState({});
  const [confirmAdoptionVisible, setConfirmAdoptionVisible] = useState(false);
  const [submissionSuccess, setSubmissionSuccess] = useState(false);

  useEffect(() => {
    if (adoptionId) {
      fetchDataAdoption();
      setSubmissionSuccess(false);
      setConfirmAdoptionVisible(true);
    }
  }, [adoptionId]);

  const fetchDataAdoption = async () => {
    if (adoptionId) {
      try {
        const adoptionResponseData = await FetchDataForAdoption(adoptionId);
        setAdoptionData(adoptionResponseData);
        console.log(adoptionResponseData.IsPreAdoptionPoll);
        console.log(adoptionResponseData.PreadoptionPoll);
        console.log(adoptionResponseData.IsMeetings);
        console.log(adoptionResponseData.IsContractAdoption);
        console.log(adoptionResponseData.ContractAdoption);
      } catch (error) {
        console.error('Adoption download error:', error);
      }
    }
  };

  const handleConfirmAdoption = async () => {
    try {
      const resp = await axios.post(
        `${address_url}/Shelters/adoptions/${adoptionId}/meetings-adoption-done`
      );

      console.log('confirmed', resp);
      setConfirmAdoption(resp);
      setSubmissionSuccess(true);
      setConfirmAdoptionVisible(false);
    } catch (err) {
      console.log(err);
    }
  };
  return (
    <div className="confirm-adoption-for-pet">
      {submissionSuccess ? (
        <>
          {' '}
          <div className="confirm-adoption-for-pet-info">
            <h2 className="confirm-adoption-for-pet-h2">
              The adoption has been successfully confirmed. You are just one
              step away from completing the adoption. Before signing the
              contract, make sure you have added your name, surname, and address
              to the register.
            </h2>
            <h2 className="confirm-adoption-for-pet-h2">
              Return to the main page.
            </h2>
          </div>
          <div className="confirm-adoption-for-pet-link">
            <Link
              to={`/Shelters/adoptions/pets/users/${userId}`}
              className="confirm-adoption-for-pet-button"
            >
              GO BACK
            </Link>
          </div>
        </>
      ) : (
        <div className="confirm-adoption-for-pet-info">
          {confirmAdoptionVisible && (
            <>
              <h2 className="confirm-adoption-for-pet-h2">
                Would you like to confirm the adoption of your new friend?
              </h2>
              <button className='confirm-adoption-for-pet-button' onClick={handleConfirmAdoption}>Confirm adoption</button>
            </>
          )}
        </div>
      )}
    </div>
  );
};

export default ConfirmAdoption;
