import { Link } from "react-router-dom";
import { signOut } from "../features/account/accountSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function Navbar() {
  const dispatch = useAppDispatch();
  const { user } = useAppSelector((state) => state.account);
  return (
    <div id="nav">
      <nav>
        <ul>
          {user ? (
            <li>
              <button onClick={() => dispatch(signOut())}>Logout</button>
            </li>
          ) : (
            <li>
              <Link to={`/login`}>Login</Link>
            </li>
          )}

          <li>
            <Link to={`/register`}>Register</Link>
          </li>
          <li>
            <Link to={`/`}>Home</Link>
          </li>
          <li>
            <Link to={`events`}>Events</Link>
          </li>
          <li>
            <Link to={`/events/create`}>Create Event</Link>
          </li>
        </ul>
      </nav>
    </div>
  );
}
