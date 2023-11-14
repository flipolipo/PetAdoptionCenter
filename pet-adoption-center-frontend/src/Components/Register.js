import React, { useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import { address_url } from '../Service/url';
import { Link } from 'react-router-dom';
import './Register.css';

Modal.setAppElement('#root');

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

const Register = () => {
  const [visible, setVisible] = useState(true);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userName, setUserName] = useState('');
  const [successRegister, setSuccessRegister] = useState(false);

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
      }
    } catch (error) {
      console.error(error);
    } finally {
      setVisible(false);
    }
  }

  return (
    <div className="signUpButton">
      <Modal
        isOpen={visible}
        onRequestClose={() => setVisible(false)}
        style={customStyles}
      >
        <div className="modal-content">
          <input
            className="input-black"
            type="username"
            placeholder="Username"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />
          <input
            className="input-black"
            type="text"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
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
          <Link to={`/`}>Home</Link>
        </div>
      </Modal>
      {successRegister && (
        <div className="register-container-info">
          <h2 className="register-success">
            You have been successfully registered
          </h2>
          <Link to={`/`} className="back-to-home">
            Back
          </Link>
        </div>
      )}
    </div>
  );
};

export default Register;
