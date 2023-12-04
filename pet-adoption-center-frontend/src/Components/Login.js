import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import { address_url } from '../Service/url';
import { useUser } from './UserContext';
import { Link, useNavigate } from 'react-router-dom';
import Avatar from 'react-avatar';
import UserRoleName from './Enum/UserRoleName';

Modal.setAppElement('#root');

const customStyles = {
  content: {
    top: '50%',
    left: '50%',
    right: 'auto',
    bottom: 'auto',
    marginRight: '-50%',
    transform: 'translate(-50%, -50%)',
    zIndex: 4
  },
};

const Login = (props) => {
  const navigate = useNavigate();
  const { user, setUser } = useUser();
  const [visible, setVisible] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const userNameToUpperCase = user.username.charAt(0).toUpperCase() + user.username.slice(1);
  const [profileData, setProfileData] = useState(null);
  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${user.id}`, {
          headers: {
            'Authorization': `Bearer ${user.token}`
          }
        });
        //console.log(response)
        setProfileData(response.data);

      } catch (err) {
        console.error(err);
      }
    };

    fetchProfileData();
  }, [user.id, user.token]);



  async function loginUser() {
    try {
      const response = await axios.post(`${address_url}/Auth/Login`, {
        Email: email,
        Password: password
      });
      if (response.status >= 200 && response.status < 300) {
        console.log('User Logged successfully!');
        //console.log(response)

        const tokenParts = response.data.Token.split('.');
        const decodedPayload = JSON.parse(atob(tokenParts[1]));
        //console.log(decodedPayload.Id)

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
    setEmail('');
    setPassword('');
    navigate('/');
    setProfileData(null)

  }

  return (

    <div className='signInButton'>
      {!user.isLogged ? (
        <>
          <button className="buttonSignIn" onClick={() => { setVisible(true); }}>Sign In</button>
          <Modal isOpen={visible} onRequestClose={() => setVisible(false)} style={customStyles}>
            <div className="modal-content">
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
              <button className="buttonLogin" onClick={loginUser}>Login</button>
              <button className="buttonLogin" onClick={() => setVisible(false)}>Back</button>
              <h5 className='need-an-account'>Need an account?</h5>
              <input className='buttonLogin' type='button' value="Sign Up!" onClick={() => {
                setVisible(false);
                props.setModalVis(true)
              }}></input>
            </div>
          </Modal>
        </>
      ) : (
        <div className='welcome-section-container'>
          <div className="welcome-section">
            <div className='icon-container'>
              {profileData.Roles?.map(role => {
                if (UserRoleName(role.Title) === 'Shelter Owner') {
                  return (
                    <div key={role.Id} className="role-shelter-owner" onClick={() => navigate(`/ShelterOwner/${profileData.ShelterId}`)}>



                      <span className="material-symbols-outlined" >
                        home
                      </span>
                      <p className="shelterOwnerText" >Your Shelter</p>

                    </div>
                  );
                } else {

                  return (
                    <></>
                  );
                }
              })}
            </div>

            <div className="icon-container">

              <Link to="/profile" className="usernameProfiles">
                <Avatar className="user-avatar" size="50" round={true} name={user.username} />
                <p>{userNameToUpperCase}</p></Link>
            </div>

            <div className="icon-container" onClick={logout}>
              <span className="material-symbols-outlined" onClick={logout} >
                logout
              </span>
              <p className="logoutText" >Logout</p>
            </div>
          </div>
        </div>
      )}
    </div>

  )
}

export default Login;