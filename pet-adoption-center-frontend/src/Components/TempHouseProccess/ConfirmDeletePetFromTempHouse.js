import React, { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import { useUser } from '../UserContext';
import { address_url } from '../../Service/url';
import { FetchTempHouseDataForUser } from '../../Service/FetchTempHouseDataForUser';
import { fetchDataForPet } from '../../Service/fetchDataForPet';
import { fetchDataForShelter } from '../../Service/fetchDataForShelter';
import axios from 'axios';

const ConfirmDeletePetFromTempHouse = () => {
    const { tempHouseId, petId } = useParams();
    const { user } = useUser();
   /*  console.log(tempHouseId);
    console.log(petId);
    console.log(user); */
    const [tempHouseData, setTempHouseData] = useState({});
    const [confirmChoosePetTempHouse, setConfirmSelectedPetTempHouse] = useState({});
    const [confirmTempHouseVisible, setConfirmTempHouseVisible] = useState(false);
    const [submissionSuccess, setSubmissionSuccess] = useState(false);
    const [deletePetFromTempHouse, setDeletePetFromTempHouse] = useState({});
    const [deleteTempHouseVisible, setDeleteTempHouseVisible] = useState(false);
    const [deleteSuccess, setDeleteSuccess] = useState(false);
    const [petData, setPetData] = useState({});
    const [shelterData, setShelterData] = useState({});
  
    useEffect(() => {
      if (petId) {
        fetchData();
        setDeleteSuccess(false);
        setSubmissionSuccess(false);
        setConfirmTempHouseVisible(true);
        setDeleteTempHouseVisible(true);
      }
    }, [petId]);
  
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
         /*  console.log(tempHouseResponseData);
          console.log(tempHouseResponseData.PetsInTemporaryHouse);
          console.log(tempHouseResponseData.IsPreTempHousePoll);
          console.log(tempHouseResponseData.IsMeetings); */
        } catch (error) {
          console.error('Temporary house download error:', error);
        }
      }
    };
  
    const fetchData = async () => {
      if (petId) {
        try {
          const petDataById = await fetchDataForPet(petId);
          setPetData(petDataById);
          //console.log(petDataById);
  
          if (petDataById && petDataById.ShelterId) {
            const shelterDataById = await fetchDataForShelter(
              petDataById.ShelterId
            );
            setShelterData(shelterDataById);
            /*  console.log(shelterDataById);
          console.log(shelterDataById.Name); */
          }
        } catch (error) {
          console.log('shelter fetch error: ' + error);
        }
      }
    };
  
    const handleConfirmSelectedPetTempHouse = async () => {
      try {
        const resp = await axios.post(
          `${address_url}/Shelters/temporary-houses/${tempHouseId}/pets/${petId}/calendar/activities/users/add-pet`
        );
  
        console.log('confirmed', resp);
        setConfirmSelectedPetTempHouse(resp);
        setSubmissionSuccess(true);
        setConfirmTempHouseVisible(false);
        setDeleteTempHouseVisible(false);
      } catch (err) {
        console.log(err);
      }
    };
    const handleDeleteSelectedPetTempHouse = async () => {
      try {
        const resp = await axios.delete(
          `${address_url}/Shelters/${shelterData.Id}/tempHouses/${tempHouseId}/pets/${petId}/users/${user.id}/delete-pet`
        );
  
        console.log('delete', resp);
        setDeletePetFromTempHouse(resp);
        setDeleteSuccess(true);
        setDeleteTempHouseVisible(false);
        setConfirmTempHouseVisible(false);
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
            The temporary housing proccess has been successfully confirmed.
            Feel free to visit the shelter in person to pick up your new
            friend. Remember to take your ID card with you.
          </h2>
          <h2 className="confirm-adoption-for-pet-h2">
            Return to the main page.
          </h2>
        </div>
        <div className="confirm-adoption-for-pet-link">
          <Link
            to={`/Shelters/temporaryHouses/${tempHouseId}/pets/users/${user.id}`}
            className="confirm-adoption-for-pet-button"
          >
            GO BACK
          </Link>
        </div>
      </>
    ) : (
      <div className="confirm-adoption-for-pet-info">
        {confirmTempHouseVisible && (
          <>
            <h2 className="confirm-adoption-for-pet-h2">
              Would you like to confirm the temporary housing proccess of your new friend?
            </h2>
            <button
              className="confirm-adoption-for-pet-button"
              onClick={handleConfirmSelectedPetTempHouse}
            >
              Confirm selected pet
            </button>
          </>
        )}
      </div>
    )}
    {deleteSuccess ? (
      <>
        {' '}
        <div className="confirm-adoption-for-pet-info">
          <h2 className="confirm-adoption-for-pet-h2">
            The temporary housing for selected pet has been successfully deleted.
          </h2>
          <h2 className="confirm-adoption-for-pet-h2">
            Return to the main page.
          </h2>
        </div>
        <div className="confirm-adoption-for-pet-link">
          <Link
            to={`/Shelters/TemporaryHouses`}
            className="confirm-adoption-for-pet-button"
          >
            GO BACK
          </Link>
        </div>
      </>
    ) : (
      <div className="confirm-adoption-for-pet-info">
        {deleteTempHouseVisible && (
          <>
            <h2 className="confirm-adoption-for-pet-h2">
              Would you like to delete the temporary housing of your new friend?
            </h2>
            <button
              className="confirm-adoption-for-pet-button"
              onClick={handleDeleteSelectedPetTempHouse}
            >
              Delete temporary house for selected pet
            </button>
          </>
        )}
      </div>
    )}
  </div>
  )
}

export default ConfirmDeletePetFromTempHouse