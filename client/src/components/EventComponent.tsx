import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Event } from "../api/models";

export default function EventComponent() {
  const { id } = useParams<{ id: string }>();
  const [event, setEvent] = useState<Event>();

  useEffect(() => {
    fetch(`http://localhost:5089/api/events/${id}`)
      .then((response) => response.json())
      .then((data) => {
        setEvent(data);
      });
  }, [id]);

  if (!event) {
    return <h1>Loading...</h1>;
  }

  console.log(event);

  return (
    <div>
      <div>
        <h3>{event.name}</h3>
      </div>
      <div>
        <img src={event.imageURL} alt={event.name} />
      </div>
      <div>Description: {event.description}</div>
      <div>Location: {event.address1}</div>
      <div>
        City, State: {event.city}, {event.state} {event.zip}
      </div>
      {event.organizer.email && <div>Organizer: {event.organizer.email}</div>}
      <div>Total attendees: {event.enrollments.length}</div>
    </div>
  );
}
