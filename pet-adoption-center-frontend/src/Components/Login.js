import React, { useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import { address_url } from '../Service/url';
import { useUser } from './UserContext';
import { Link } from 'react-router-dom';

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

const Login = () => {
  const { user, setUser } = useUser();
  const [visible, setVisible] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  async function loginUser() {
    try {
      const response = await axios.post(`${address_url}/Auth/Login`, {
        Email: email,
        Password: password
      });
      if (response.status >= 200 && response.status < 300) {
        console.log('User Logged successfully!');
        console.log(response)

        const tokenParts = response.data.Token.split('.');
        const decodedPayload = JSON.parse(atob(tokenParts[1]));
        console.log(decodedPayload.Id)

        setUser({
          id: decodedPayload.Id,
          username: response.data.UserName,
          email: response.data.Email,
          refreshtoken: response.data.RefreshToken,
          token: response.data.Token,
          isLogged: true
        });
        setVisible(false);
      }
    }
    catch (error) {
      console.error(error);
    }
  }

  function logout() {
    setUser({
      id: '',
      username: '',
      email: '',
      refreshtoken: '',
      token: '',
      isLogged: false
    });
  }

  return (
    <div className='signInButton'>
      {!user.isLogged ? (
        <>
          <button onClick={() => { setVisible(true); }}>Sign In</button>
          <Modal isOpen={visible} onRequestClose={() => setVisible(false)} style={customStyles}>
            <input
              className="input-black"
              type="text"
              placeholder="Email"
              value={email}
              onChange={e => setEmail(e.target.value)}
            />
            <input
              className="input-black"
              type="password"
              placeholder="Password"
              value={password}
              onChange={e => setPassword(e.target.value)}
            />
            <button onClick={loginUser}>Login</button>
            <button onClick={() => setVisible(false)}>Back</button>
          </Modal>
        </>
      ) : (
        <>
          <h2>Welcome, <Link to="/profile">{user.username}</Link>!</h2>
          <button onClick={logout}>Logout</button>
        </>
      )}
    </div>
  )
}

export default Login;