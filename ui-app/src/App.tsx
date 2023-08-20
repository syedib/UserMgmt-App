import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import UserList from './components/user-list.component';
import UserComponent from './components/user.component';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<UserList />}/>
        <Route path='add-user' element={<UserComponent/>}/>
        <Route path='add-user/:id' element={<UserComponent/>}/>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
