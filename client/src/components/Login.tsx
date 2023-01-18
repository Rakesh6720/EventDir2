import { useForm, SubmitHandler, FieldValues } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { signInUser } from "../features/account/accountSlice";
import { useAppDispatch } from "../store/configureStore";

type Inputs = {
  username: string;
  password: string;
};

export default function Login() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors, isValid },
  } = useForm<Inputs>({
    mode: "all",
  });

  const onSubmit: SubmitHandler<Inputs> = async (data: FieldValues) => {
    await dispatch(signInUser(data));
    navigate("/");
  };

  return (
    <>
      <h1>Login</h1>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div>
          <label htmlFor="username">Username: </label>
          <input
            title="username"
            {...register("username", { required: "A username is required" })}
          />
          {!!errors.username && <p>{errors?.username?.message}</p>}
        </div>
        <div>
          <label htmlFor="password">Password: </label>
          <input
            type="password"
            title="password"
            {...register("password", { required: "A password is required" })}
          />
          {!!errors.password && <p>{errors?.password?.message}</p>}
        </div>
        <button type="submit" disabled={!isValid}>
          Sign In
        </button>
      </form>
    </>
  );
}
