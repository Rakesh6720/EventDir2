import { useEffect, useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { useAppSelector } from "../store/configureStore";
import "./CreateEvent.css";

type Event = {
  id: string;
  name: string;
  description: string;
  address1: string;
  address2: string;
  city: string;
  state: string;
  zip: string;
  imageURL: string;
  organizerEmail: string;
};

export default function CreateEvent() {
  const [newEvent, setNewEvent] = useState<Event>();
  const user = useAppSelector((state) => state.account.user)
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm<Event>();
  const onSubmit: SubmitHandler<Event> = async (data: Event) => {
    fetch("http://localhost:5089/api/events", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${user?.token}`
      },
      body: JSON.stringify(data),
    })
      .then((response) => response.json())
      .then((data: Event) => {
        console.log(`Success: ${data}`);
        setNewEvent(data);
      })
      .catch((error) => {
        console.log(`Error: ${error}`);
      });
    // redirect to created event page
  };

  useEffect(() => {
    if (newEvent) {
      const response = async () =>
        await fetch(`http://localhost:5089/api/events/${newEvent?.id}`)
          .then((response) => response.json())
          .then((data) =>
            console.log(`New Event Info: ${Object.entries(data)}`)
          );
      response();
    }
  }, [newEvent]);

  return (
    <>
      <h1>Create Event</h1>
      <div>
        <form
          onSubmit={handleSubmit(onSubmit)}
          style={{ display: "flex", flexDirection: "column" }}
        >
          <label>
            Name:
            <input {...register("name")} />
          </label>
          <label>
            Description:
            <input {...register("description")} />
          </label>
          <label>
            Address 1:
            <input {...register("address1")} />
          </label>
          <label>
            Address 2:
            <input {...register("address2")} />
          </label>
          <label>
            City:
            <input {...register("city")} />
          </label>
          <label>
            State:
            <input {...register("state")} />
          </label>
          <label>
            Zip:
            <input {...register("zip")} />
          </label>
          <label>
            Organizer Email:
            <input {...register("organizerEmail")} />
          </label>
          <label>
            Image URL:
            <input {...register("imageURL")} />
          </label>

          <input type="submit" style={{ margin: "1em", width: "25%" }} />
        </form>
      </div>
    </>
  );
}
