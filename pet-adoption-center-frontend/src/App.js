import './App.css';
import Home from './Pages/Home/Home';
import Shelters from './Pages/Shelters/Shelters';
import Adoption from './Pages/Adoption/Adoption';
import TemporaryHouse from './Pages/TemporaryHouse/TemporaryHouse';
import Pets from './Pages/Pets/Pets';
import Profile from './Pages/Profile/Profile';
import PetById from './Pages/Pets/PetsById/PetById';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './Components/Navbar';
import ScrollHandler from './Components/ScrollHandler';
import Footer from './Components/Footer';
import React, { useState } from 'react';
import { UserProvider } from './Components/UserContext.js';
import ShelterOwner from './Pages/Profile/ShelterOwner';
import PreadoptionPoll from './Components/PreadoptionPoll.js';
import Meetings from './Components/Meetings.js';
import AdoptionById from './Pages/Adoption/AdoptionById.js/AdoptionById.js';
import ContractAdoption from './Components/ContractAdoption.js';
import ConfirmAdoption from './Components/ConfirmAdoption.js';
import Register from './Components/Register.js';
import PreadoptionPollInfo from './Components/PreadoptionPollInfo.js';
import MeetingsInfo from './Components/MeetingsInfo.js';
import ContractAdoptionInfo from './Components/ContractAdoptionInfo.js';
import ShelterById from './Pages/Shelters/ShelterById.js';

function App() {
  const [petData, setPetData] = useState([]);
  return (
    <Router>
      <UserProvider>
        <div className="App-container">
          <ScrollHandler />
          <Navbar />
          <div className="routes">
            <Routes>
              <Route exact path="/user/register" element={<Register />} />
              <Route exact path="/" element={<Home />} />
              <Route path="/Shelters" element={<Shelters />} />
              <Route
                path="/Shelters/adoptions"
                element={<Adoption petData={petData} setPetData={setPetData} />}
              />
              <Route
                path="/Shelters/adoptions/preadoption-poll"
                element={<PreadoptionPollInfo />}
              />
                <Route
                path="/Shelters/adoptions/meetings-info"
                element={<MeetingsInfo />}
              />
                  <Route
                path="/Shelters/adoptions/contract-adoption-info"
                element={<ContractAdoptionInfo />}
              />
              <Route
                path="/Shelters/adoptions/pets/:id"
                element={<Adoption />}
              />
              <Route
                path="/Shelters/adoptions/pets/:id/users/:userId"
                element={<PreadoptionPoll />}
              />
              <Route
                path="/Shelters/adoptions/pets/users/:userId"
                element={<Meetings />}
              />
              <Route
                path="/Shelters/adoptions/:adoptionId/pets/:petId/users/:userId"
                element={<AdoptionById />}
              />
              <Route
                path="/Shelters/adoptions/:adoptionId/pets/:petId/users/:userId/contract-adoption"
                element={<ContractAdoption />}
              />
              <Route
                path="/Shelters/adoptions/:adoptionId/pets/:petId/users/:userId/confirm-adoption"
                element={<ConfirmAdoption />}
              />
              <Route
                path="/Shelters/temporaryHouses"
                element={<TemporaryHouse />}
              />
              <Route path="/Users/pets" element={<Pets />} />
              <Route
                path="/Users/pets/:id"
                element={<PetById petData={petData} setPetData={setPetData} />}
              />
              <Route path="/profile" element={<Profile />} />
              <Route
                path="/ShelterOwner/:shelterId"
                element={<ShelterOwner />}
              />
              <Route path="/Shelters/:shelterId" element={<ShelterById />} />
            </Routes>
          </div>

          <Footer />
        </div>
      </UserProvider>
    </Router>
  );
}

export default App;
