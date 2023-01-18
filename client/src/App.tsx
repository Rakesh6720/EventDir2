import { useEffect } from "react";
import { Outlet } from "react-router-dom";
import "./App.css";
import Navbar from "./components/Navbar";
import { fetchCurrentUser } from "./features/account/accountSlice";
import { useAppDispatch } from "./store/configureStore";

function App() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchCurrentUser());
  }, [dispatch])
  return (
    <>
      <Navbar />
      <div id="details">
        <Outlet />
      </div>
    </>
  );
}

export default App;
