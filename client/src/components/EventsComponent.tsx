import { useEffect, useState } from "react";
import { Event } from "../api/models";
import { Link } from "react-router-dom";

export default function EventsComponent() {
  const [events, setEvents] = useState<Event[]>();

  useEffect(() => {
    fetch("http://localhost:5089/api/events")
      .then((response) => response.json())
      .then((data) => setEvents(data));
  }, []);

  if (!events) {
    return <h1>Loading...</h1>;
  }

  return (
    <div>
      <h1>Events</h1>
      {events &&
        events.map((event) => (
          <div key={event.id}>
            <h3>
              <Link to={`/event/${event.id}`}>{event.name}</Link>
            </h3>
            <img src={event.imageURL} alt={event.name} />
            <ul>
              <li>Description: {event.description}</li>
              <li>Address: {event.address1}</li>
              <li>
                City, State, Zip: {event.city}, {event.state} {event.zip}
              </li>
              {/* {event.organizer.email && (
                <li>Contact: {event.organizer.email}</li>
              )} */}
            </ul>
          </div>
        ))}
    </div>
  );
}
