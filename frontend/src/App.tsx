import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from './pages/Login';
import Dashboard from "./pages/Dashboard";
import Home from "./pages/Home";

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/dashboard" element={<Home />} />
          <Route path="/dashboard/:idAccount" element={<Dashboard />}/>
        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
