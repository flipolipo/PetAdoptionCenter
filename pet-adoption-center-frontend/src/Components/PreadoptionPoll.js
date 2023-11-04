import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { address_url } from '../Service/url';
import { Link, useParams } from 'react-router-dom';
import { fetchDataForPet } from '../Service/fetchDataForPet';
import './PreadoptionPoll.css';

const PreadoptionPoll = () => {
  const { id, userId } = useParams();
  console.log(id);
  console.log(userId);

  const [formData, setFormData] = useState({
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
  const [preadoptionPollData, setPreadoptionPollData] = useState('');
  const [submissionSuccess, setSubmissionSuccess] = useState(false);
  const [petSelectedData, setPetSelectedData] = useState({});
  const [preadoptionPollVisible, setPreadoptionPollVisible] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const responseData = await fetchDataForPet(id);
        setPetSelectedData(responseData);
        console.log(responseData.ShelterId);
        setSubmissionSuccess(false);
        setPreadoptionPollVisible(true);
      } catch (err) {
        console.log('shelter fetch error: ' + err);
      }
    };
    fetchData();
  }, []);
  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "numAdults" || name === "timePetLeftAlone" || name === "petIndoorsTime") {
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
    e.preventDefault();
    const errors = {};
    if (formData.over18 !== 'Yes') {
      errors.over18 = 'You must be 18 years or older to submit this form.';
    }
    for (const key in formData) {
      if (!formData[key]) {
        errors[key] = 'This field is required.';
      }
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors);
    } else {
      const formDataString = `Over 18: ${formData.over18}, Willing to Invest: ${formData.willingToInvest}, Resitenze type: ${formData.residenceType}, 
      Fenced back yard : ${formData.fencedBackyard}, Number of Adults: ${formData.numAdults}, Household members want pet: ${formData.householdMembersWantPet},
      Relinquished pets before: ${formData.relinquishedPetsBefore}, Current pets list: ${formData.currentPetsList},
      Time pet left alone: ${formData.timePetLeftAlone}, Pet location when home: ${formData.petLocationWhenHome},
      Pet location when not home: ${formData.petLocationWhenNotHome}, Pet indoors time: ${formData.petIndoorsTime},
      Has veterinarian: ${formData.hasVeterinarian}, Vacation arrangements: ${formData.vacationArrangements}`;
      console.log(formDataString);
      const requestData = {
        isPreAdoptionPoll: true,
        preadoptionPoll: formDataString,
        activity: {},
      };
      console.log(requestData);
      try {
        const response = await axios.post(
          `${address_url}/Shelters/${petSelectedData.ShelterId}/pets/${id}/users/${userId}/adoptions/inizialize-adoption`,
          requestData
        );
        setPreadoptionPollData(formDataString);
        console.log(response);
        setSubmissionSuccess(true);
        setPreadoptionPollVisible(false);
      } catch (error) {}
    }
  };

  return (
    <div className="preadoption-form">
      {submissionSuccess ? (
        <>
        <div className='preadoption-poll-form-info'>
          <h2 className='preadoption-poll-h2'>The pre-adoption poll has been successfully sent. </h2>
          <h2 className='preadoption-poll-h2'> Return to the main
            page to schedule a meeting with the pet.</h2>
            </div>
            <div className='preadoption-poll-find-pet-container'>
          <Link to={`/Shelters/adoptions/pets/${id}`} className='preadoption-poll-find-pet'>GO BACK</Link>
          </div>
        </>
      ) : (
        <>
          {preadoptionPollVisible && (
            <>
              {' '}
              <form onSubmit={handleSubmit}>
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
                <button type="submit">Submit</button>
              </form>
            </>
          )}
        </>
      )}
    </div>
  );
};

export default PreadoptionPoll;
