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
import React from 'react';
import { UserProvider } from './Components/UserContext.js';
import ShelterById from './Pages/Shelters/ShelterById';


function App() {

  return (
    <Router>
      <UserProvider>
        <div className="App-container">
          <ScrollHandler />
          <Navbar />

          <div className="routes">
            <Routes>
              <Route exact path="/" element={<Home />} />
              <Route path="/Shelters" element={<Shelters />} />
              <Route path="/Shelters/adoptions" element={<Adoption />} />
              <Route
                path="/Shelters/temporaryHouses"
                element={<TemporaryHouse />}
              />
              <Route path="/Users/pets" element={<Pets />} />
              <Route path="/Users/pets/:id" element={<PetById />} />
              <Route path="/Profile" element={<Profile />} />
              <Route path="/Shelters/:id" element={<ShelterById />} />
            </Routes>
          </div>
          <Footer />
        </div>
      </UserProvider>
    </Router>
  );
}

export default App;