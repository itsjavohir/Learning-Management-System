import { BrowserRouter, Routes, Route } from 'react-router-dom';
import UsersPage from '../pages/users-page/UsersPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<UsersPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;