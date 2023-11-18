import React, { useState, useEffect } from 'react';
import './PreTemporaryHousePoll.css';
import { Link, useParams } from 'react-router-dom';
import { FaSpinner } from 'react-icons/fa';
import { fetchDataForPet } from '../../Service/fetchDataForPet';
import axios from 'axios';
import { address_url } from '../../Service/url';

const PreTemporaryHousePoll = () => {
  const { shelterId, petId, userId } = useParams();
 /*  console.log(shelterId);
  console.log(petId);
  console.log(userId); */

  const [formData, setFormData] = useState({
    tempOwnerName: '',
    tempOwnerSurame: '',
    tempOwnerCity: '',
    tempOwnerStreet: '',
    tempOwnerHouseNumber: '',
    tempOwnerFlatNumber: '',
    tempOwnerPostalCode: '',
    over18: '',
    willingToInvest: '',
    residenceType: '',
    fencedBackyard: '',
    numAdults: '',
    householdMembersWantPet: '',
    relinquishedPetsBefore: '',
    currentPetsList: '',
    timePetLeftAlone: '',
    petLocationWhenHome: '',
    petLocationWhenNotHome: '',
    petIndoorsTime: '',
    hasVeterinarian: '',
    vacationArrangements: '',
  });

  const [formErrors, setFormErrors] = useState({});
  const [preTempHousePollData, setPreTempHousePollData] = useState('');
  const [submissionSuccess, setSubmissionSuccess] = useState(false);
  const [petSelectedData, setPetSelectedData] = useState({});
  const [preTempHousePollVisible, setPreTempHousePollVisible] = useState(false);
  const [loading, setLoading] = useState(false);
  const [formReady, setFormReady] = useState(false);
  const [userData, setUserData] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      try {
        const responseData = await fetchDataForPet(petId);
        setPetSelectedData(responseData);
        // console.log(responseData.ShelterId);
        setSubmissionSuccess(false);
        setPreTempHousePollVisible(true);
      } catch (err) {
        console.log('shelter fetch error: ' + err);
      }
    };
    fetchData();
  }, []);

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${userId}`, {
          headers: {
            Authorization: `Bearer ${userId.token}`,
          },
        });
        setUserData(response.data);
        setSubmissionSuccess(false);
        setPreTempHousePollVisible(true);
        //console.log(response.data.BasicInformation.Name);
        //console.log(response.data.BasicInformation.Surname);
      } catch (err) {
        console.log(err);
      }
    };

    fetchProfileData();
  }, [userId, userId.token]);

  useEffect(() => {
    const errorsExist = Object.values(formErrors).some((error) => error !== '');
    //console.log('formErrors:', formErrors);
    //console.log('formReady:', !errorsExist);
    setFormReady(!errorsExist);
  }, [formErrors]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (
      name === 'numAdults' ||
      name === 'timePetLeftAlone' ||
      name === 'petIndoorsTime'
    ) {
      if (isNaN(value) || Number(value) < 0) {
        setFormErrors({
          ...formErrors,
          [name]: 'Please enter a non-negative number.',
        });
      } else {
        setFormErrors({
          ...formErrors,
          [name]: '',
        });
        setFormData({
          ...formData,
          [name]: value,
        });
      }
    } else {
      setFormData({
        ...formData,
        [name]: value,
      });
      setFormErrors({
        ...formErrors,
        [name]: '',
      });
    }
  };

  const handleSubmit = async (e) => {
    setLoading(true);
    e.preventDefault();
    const errors = {};
    if (
      formData.tempOwnerName.trim() !== userData.BasicInformation.Name.trim()
    ) {
      errors.tempOwnerName = 'Wrong name to submit this form.';
    }
    if (
      formData.tempOwnerSurame.trim() !==
      userData.BasicInformation.Surname.trim()
    ) {
      errors.tempOwnerSurame = 'Wrong surname to submit this form.';
    }
    if (
      formData.tempOwnerCity.trim() !==
      userData.BasicInformation.Address.City.trim()
    ) {
      errors.tempOwnerCity = 'Wrong city name to submit this form.';
    }
    if (
      formData.tempOwnerStreet.trim() !==
      userData.BasicInformation.Address.Street.trim()
    ) {
      errors.tempOwnerStreet = 'Wrong street name to submit this form.';
    }
    if (
      formData.tempOwnerPostalCode.trim() !==
      userData.BasicInformation.Address.PostalCode.trim()
    ) {
      errors.tempOwnerPostalCode = 'Wrong street name to submit this form.';
    }
    for (const key in formData) {
      if (key !== 'tempOwnerFlatNumber' && !formData[key].trim()) {
        errors[key] = 'This field is required.';
      }
    }

    if (
      'tempOwnerFlatNumber' in formData &&
      formData.tempOwnerFlatNumber &&
      !formData.tempOwnerFlatNumber.trim()
    ) {
      errors.tempOwnerFlatNumber = 'Flat number must be a number.';
    }

    if (formData.over18 !== 'Yes') {
      errors.over18 = 'You must be 18 years or older to submit this form.';
    }

    if (!formReady) {
      setLoading(false);
      return;
    }
    setTimeout(() => {
      setLoading(false);
    }, 5000);

    if (Object.keys(errors).length > 0) {
      setFormErrors(errors);
    } else {
      const formDataString = `Temporary house owner: name: ${formData.tempOwnerName}, surname: ${formData.tempOwnerSurame},
      Address: street name: ${formData.tempOwnerCity}, house number: ${formData.tempOwnerHouseNumber}, 
      flat number: ${formData.tempOwnerFlatNumber}, city name: ${formData.tempOwnerCity}, postal code:
      ${formData.tempOwnerPostalCode}. Over 18: ${formData.over18}, Willing to Invest: ${formData.willingToInvest}, Resitenze type: ${formData.residenceType}, 
      Fenced back yard : ${formData.fencedBackyard}, Number of Adults: ${formData.numAdults}, Household members want pet: ${formData.householdMembersWantPet},
      Relinquished pets before: ${formData.relinquishedPetsBefore}, Current pets list: ${formData.currentPetsList},
      Time pet left alone: ${formData.timePetLeftAlone}, Pet location when home: ${formData.petLocationWhenHome},
      Pet location when not home: ${formData.petLocationWhenNotHome}, Pet indoors time: ${formData.petIndoorsTime},
      Has veterinarian: ${formData.hasVeterinarian}, Vacation arrangements: ${formData.vacationArrangements}`;
      console.log(formDataString);
      const requestData = {
        isPreTempHousePoll: true,
        tempHousePoll: formDataString,
        activity: {},
      };
      console.log(requestData);
      try {
        const response = await axios.post(
          `${address_url}/Shelters/${shelterId}/temporary-houses/pets/${petId}/users/${userId}`,
          requestData
        );
        setPreTempHousePollData(formDataString);
        console.log(response);
        setSubmissionSuccess(true);
        setPreTempHousePollVisible(false);
      } catch (error) {
        console.error('Error submitting form:', error);
      } finally {
        setLoading(false);
      }
    }
  };

  return (
    <div className="preadoption-form">
      {submissionSuccess ? (
        <>
          <div className="preadoption-poll-form-info">
            <h2 className="preadoption-poll-h2">
              The pre-temporary-housing poll has been successfully sent.{' '}
            </h2>
            <h2 className="preadoption-poll-h2">
              {' '}
              Return to the main page to schedule a meeting with the pet.
            </h2>
          </div>
          <div className="preadoption-poll-find-pet-container">
            <Link
              to={`/Shelters/${shelterId}/temporaryHouses/pets/${petId}`}
              className="preadoption-poll-find-pet"
            >
              GO BACK
            </Link>
          </div>
        </>
      ) : (
        <>
          {preTempHousePollVisible && (
            <>
              <div className="temporary-house-basic-info">
                <h2 className="important">
                  Have you filled in your personal details (name, surname,
                  address)? This is necessary to complete the pre temporary
                  house poll. If not click on the botton:
                </h2>
                {userId && (
                  <Link to={`/profile`} className="find-pet-link">
                    Basic information
                  </Link>
                )}
              </div>{' '}
              <form onSubmit={handleSubmit}>
                <label>Name: *</label>
                <input
                  type="text"
                  name="tempOwnerName"
                  value={formData.tempOwnerName}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.tempOwnerName && (
                  <p className="error-message">{formErrors.tempOwnerName}</p>
                )}
                <label>Surname: *</label>
                <input
                  type="text"
                  name="tempOwnerSurame"
                  value={formData.tempOwnerSurame}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.tempOwnerSurame && (
                  <p className="error-message">{formErrors.tempOwnerSurame}</p>
                )}
                <label>Address : Street name: *</label>
                <input
                  type="text"
                  name="tempOwnerStreet"
                  value={formData.tempOwnerStreet}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.tempOwnerStreet && (
                  <p className="error-message">{formErrors.tempOwnerStreet}</p>
                )}
                <label>House number: *</label>
                <input
                  type="text"
                  name="tempOwnerHouseNumber"
                  value={formData.tempOwnerHouseNumber}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.tempOwnerHouseNumber && (
                  <p className="error-message">
                    {formErrors.tempOwnerHouseNumber}
                  </p>
                )}
                <label>Flat number: *</label>
                <input
                  type="text"
                  name="tempOwnerFlatNumber"
                  value={formData.tempOwnerFlatNumber}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.tempOwnerFlatNumber && (
                  <p className="error-message">
                    {formErrors.tempOwnerFlatNumber}
                  </p>
                )}
                <label>City: *</label>
                <input
                  type="text"
                  name="tempOwnerCity"
                  value={formData.tempOwnerCity}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.tempOwnerCity && (
                  <p className="error-message">{formErrors.tempOwnerCity}</p>
                )}
                <label>Postal code: *</label>
                <input
                  type="text"
                  name="tempOwnerPostalCode"
                  value={formData.tempOwnerPostalCode}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.tempOwnerPostalCode && (
                  <p className="error-message">
                    {formErrors.tempOwnerPostalCode}
                  </p>
                )}
                <label>Are you 18 years or over?*</label>
                <select
                  name="over18"
                  onChange={handleChange}
                  value={formData.over18}
                >
                  <option value="">Select</option>
                  <option value="Yes">Yes</option>
                  <option value="No">No</option>
                </select>
                {formErrors.over18 && (
                  <p className="error-message">{formErrors.over18}</p>
                )}
                <label>
                  Are you willing to make the investment in both time and
                  finances to properly care for and manage your new pet?*
                </label>
                <select
                  name="willingToInvest"
                  onChange={handleChange}
                  value={formData.willingToInvest}
                >
                  <option value="">Select</option>
                  <option value="Yes">Yes</option>
                  <option value="No">No</option>
                </select>
                {formErrors.willingToInvest && (
                  <p className="error-message">{formErrors.willingToInvest}</p>
                )}
                <label>Do you live in a:* </label>
                <select
                  name="residenceType"
                  onChange={handleChange}
                  value={formData.residenceType}
                >
                  <option value="">Select</option>
                  <option value="House">House</option>
                  <option value="Condo/Townhouse">Condo/Townhouse</option>
                  <option value="Apartment">Apartment</option>
                </select>
                {formErrors.residenceType && (
                  <p className="error-message">{formErrors.residenceType}</p>
                )}
                <label>Is there a fenced back yard?* </label>
                <select
                  name="fencedBackyard"
                  onChange={handleChange}
                  value={formData.fencedBackyard}
                >
                  <option value="">Select</option>
                  <option value="Yes">Yes</option>
                  <option value="No">No</option>
                </select>
                {formErrors.fencedBackyard && (
                  <p className="error-message">{formErrors.fencedBackyard}</p>
                )}
                <label>How many adults are in your household?*</label>
                <input
                  type="number"
                  name="numAdults"
                  value={formData.numAdults}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.numAdults && (
                  <p className="error-message">{formErrors.numAdults}</p>
                )}
                <label>
                  Do ALL the members of your household want a new pet?*
                </label>
                <select
                  name="householdMembersWantPet"
                  onChange={handleChange}
                  value={formData.householdMembersWantPet}
                >
                  <option value="">Select</option>
                  <option value="Yes">Yes</option>
                  <option value="No">No</option>
                </select>
                {formErrors.householdMembersWantPet && (
                  <p className="error-message">
                    {formErrors.householdMembersWantPet}
                  </p>
                )}
                <label>
                  Have you relinquished or given away any pets before?*
                </label>
                <select
                  name="relinquishedPetsBefore"
                  onChange={handleChange}
                  value={formData.relinquishedPetsBefore}
                >
                  <option value="">Select</option>
                  <option value="Yes">Yes</option>
                  <option value="No">No</option>
                </select>
                {formErrors.relinquishedPetsBefore && (
                  <p className="error-message">
                    {formErrors.relinquishedPetsBefore}
                  </p>
                )}
                <label>
                  Please list current pets residing at your home (including
                  roommates' pets also). Include pets' breed, age, sex,
                  spay/neuter status, number of years owned, and if they live
                  indoors or outdoors. *
                </label>
                <input
                  type="text"
                  name="currentPetsList"
                  value={formData.currentPetsList}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.currentPetsList && (
                  <p className="error-message">{formErrors.currentPetsList}</p>
                )}
                <label>
                  In a 24-hour day, how long would the pet be left alone at a
                  given time?*{' '}
                </label>
                <input
                  type="number"
                  name="timePetLeftAlone"
                  value={formData.timePetLeftAlone}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.timePetLeftAlone && (
                  <p className="error-message">{formErrors.timePetLeftAlone}</p>
                )}
                <label>
                  Where will your new pet be kept when you are home?*{' '}
                </label>
                <input
                  type="text"
                  name="petLocationWhenHome"
                  value={formData.petLocationWhenHome}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.petLocationWhenHome && (
                  <p className="error-message">
                    {formErrors.petLocationWhenHome}
                  </p>
                )}
                <label>
                  Where will your new pet be kept when you are NOT at home?*{' '}
                </label>
                <input
                  type="text"
                  name="petLocationWhenNotHome"
                  value={formData.petLocationWhenNotHome}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.petLocationWhenNotHome && (
                  <p className="error-message">
                    {formErrors.petLocationWhenNotHome}
                  </p>
                )}
                <label>
                  In a 24-hour day, how long would the animal be indoors?*{' '}
                </label>
                <input
                  type="number"
                  name="petIndoorsTime"
                  value={formData.petIndoorsTime}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.petIndoorsTime && (
                  <p className="error-message">{formErrors.petIndoorsTime}</p>
                )}
                <label>
                  Do you already have a veterinarian? If yes, Name:*{' '}
                </label>
                <input
                  type="text"
                  name="hasVeterinarian"
                  value={formData.hasVeterinarian}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.hasVeterinarian && (
                  <p className="error-message">{formErrors.hasVeterinarian}</p>
                )}
                <label>
                  What arrangements would you make for your new animal if you
                  went on a trip/vacation?*
                </label>
                <input
                  type="text"
                  name="vacationArrangements"
                  value={formData.vacationArrangements}
                  onChange={handleChange}
                  className="form-preadoptionPoll"
                />
                {formErrors.vacationArrangements && (
                  <p className="error-message">
                    {formErrors.vacationArrangements}
                  </p>
                )}
                <div className="spinner-container">
                  <button type="submit" disabled={loading || !formReady}>
                    {loading && formReady ? (
                      <FaSpinner className="spinner" />
                    ) : (
                      'Submit'
                    )}
                  </button>
                  {!formReady && (
                    <p className="error-message">
                      Check if all fields are filled.
                    </p>
                  )}
                </div>
              </form>
            </>
          )}
        </>
      )}
    </div>
  );
};

export default PreTemporaryHousePoll;
