import React, { useState } from 'react';
import { Link, NavLink } from 'react-router-dom';
import './Navbar.css';
import Login from './Login';
import { useUser } from './UserContext';
import axios from 'axios';
import { address_url } from '../Service/url';
import Modal from 'react-modal';

Modal.setAppElement('#root');

const Navbar = () => {
  const [menuOpen, setMenuOpen] = useState(false);
  const { user, setUser } = useUser();

  const handleClick = () => setMenuOpen(!menuOpen);
  const [visible, setVisible] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userName, setUserName] = useState('');
  
  const [successRegister, setSuccessRegister] = useState(false);
  const [invalidUsername, setInvalidUsername] = useState(false)
  const [invalidEmail, setInvalidEmail] = useState(false)
  const [takenEmail, setTakenEmail] = useState(false)
  const customStyles = {
    content: {
      top: '50%',
      left: '50%',
      right: 'auto',
      bottom: 'auto',
      marginRight: '-50%',
      transform: 'translate(-50%, -50%)',
    },
  };
  async function registerUser() {
    try {
      const response = await axios.post(`${address_url}/Auth/Register`, {
        Username: userName,
        Email: email,
        Password: password,
      });
      if (response.status >= 200 && response.status < 300) {
        console.log('User registered successfully!');
        console.log(response);
        setSuccessRegister(true);
        setInvalidUsername(false)
        setInvalidEmail(false)
        setTakenEmail(false)
      }
    } catch (error) {

      console.error(error.response.data);
      if(error.response.data.DuplicateEmail)
      {
        setTakenEmail(true)
      }
      if(error.response.data.DuplicateUserName){
        setInvalidUsername(true)
      }
      if(error.response.data.InvalidEmail){
        setInvalidEmail(true)
      }
    }
  }

  return (
    <>
      <nav className="navbar">
        <div className="navbar-container">
          <img
            className="navbar-logo"
            alt=""
            src={process.env.PUBLIC_URL + '/Photo/Round_full.png'}
            style={{ width: '120px', height: 'auto' }}
          />
          <span></span>
          <span></span>
          <span></span>
          <div className="user-auth-div">
            <Login setModalVis={setVisible} className="LoginComponent" user={user} setUser={setUser} />
            {!user.isLogged && (
              <div className="signUpButton">
                <input
                  onClick={() => setVisible(true)}
                  type="button"
                  className="find-pet-link-2"
                  value="Sign Up"
                ></input>
                <Modal
                  isOpen={visible}
                  onRequestClose={() => setVisible(false)}
                  style={customStyles}
                >
                  {successRegister ? (
                    <div className="register-container-info">
                      <h2 className="register-success">
                        You have been successfully registered
                      </h2>
                      <input
                        value="Back"
                        className="back-to-home"
                        onClick={() => setVisible(false)}
                      ></input>
                    </div>
                  ) : (
                    <div className="modal-content">
                      <input
                        className="input-black"
                        type="username"
                        placeholder="Username"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                      />
                       {invalidUsername && <h5 className='error-msg'>Username already taken!</h5>}
                      <input
                        className="input-black"
                        type="text"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                      />
                      {takenEmail && <h5 className='error-msg'>Mail already taken!</h5>}
                      {invalidEmail && <h5 className='error-msg'>Provide correct email format!</h5>}
                      <input
                        className="input-black"
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                      />
                     
                      <button className="buttonRegister" onClick={registerUser}>
                        Register
                      </button>
                      <input
                        type="button"
                        value="Back"
                        className="back-to-home"
                        onClick={() => setVisible(false)}
                      ></input>
                    </div>
                  )}
                </Modal>
              </div>
            )}
          </div>
        </div>
        <div className="menu" onClick={handleClick}>
          <span></span>
          <span></span>
          <span></span>
        </div>
        <div className="menu-icon" onClick={handleClick}>
          <ul className={menuOpen ? 'open' : ''}>
            <li className="nav-item-navigation">
              <Link to="/" className="nav-links">
                HOME
              </Link>
            </li>
            <li className="nav-item-navigation">
              <NavLink to="/Shelters/Adoptions" className="nav-links">
                ADOPTION
              </NavLink>
            </li>
            <li className="nav-item-navigation">
              <NavLink to="/Shelters" className="nav-links">
                SHELTERS
              </NavLink>
            </li>
            <li className="nav-item-navigation">
              <NavLink to="/Shelters/TemporaryHouses" className="nav-links">
                TEMPORARY HOUSE
              </NavLink>
            </li>
            <li className="nav-item-navigation">
              <NavLink to="/Users/pets" className="nav-links">
                PETS
              </NavLink>
            </li>
          </ul>
        </div>
      </nav>
    </>
  );
};

export default Navbar;
