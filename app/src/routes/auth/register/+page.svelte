<script lang="ts">
  import { goto } from "$app/navigation";
  import { authStore, setAuthState } from "$lib/stores/authStore";

  let firstName = "";
  let lastName = "";
  let email = "";
  let password = "";
  let passwordConfirm = "";
  let errorMessage = "";

  const handleRegister = async () => {
    errorMessage = "";

    if(password != passwordConfirm){
        errorMessage = 'Passwords do not match';
        return;
    }

    try {
      const respose = await fetch(
        `${import.meta.env.VITE_API_URL}/auth/register`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ firstName, lastName, email, password }),
        }
      );

      if (respose.ok) {
        goto("/auth/login");
      } else {
        // this will happen in a good environment (that message will be dispatched by our API)
        const errorData = await respose.json();
        errorMessage = errorData.message || "Registration failed, Please try again";
      }
    } catch (error) {
      // this happen becasue something wronh with API or Network connection
      errorMessage = "An error occured. Please try again later.";
      console.error("Login error: ", error);
    }
  };
</script>

<div class="col-lg-7">
  <div class="card shadow-lg border-0 rounded-lg mt-5">
    <div class="card-header">
      <h3 class="text-center font-weight-light my-3">Create Account</h3>
    </div>
    <div class="card-body">
      <form on:submit|preventDefault={handleRegister}>
        <div class="row mb-3">
          <div class="col-md-6">
            <div class="form-floating mb-3 mb-md-0">
              <input
                class="form-control"
                id="inputFirstName"
                type="text"
                placeholder="Enter your first name"
                bind:value={firstName}
              />
              <label for="inputFirstName">First name</label>
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-floating">
              <input
                class="form-control"
                id="inputLastName"
                type="text"
                placeholder="Enter your last name"
                bind:value={lastName}
              />
              <label for="inputLastName">Last name</label>
            </div>
          </div>
        </div>
        <div class="form-floating mb-3">
          <input
            class="form-control"
            id="inputEmail"
            type="email"
            placeholder="name@example.com"
            bind:value={email}
            required
          />
          <label for="inputEmail">Email address</label>
        </div>
        <div class="row mb-3">
          <div class="col-md-6">
            <div class="form-floating mb-3 mb-md-0">
              <input
                class="form-control"
                id="inputPassword"
                type="password"
                placeholder="Create a password"
                bind:value={password}
                required
              />
              <label for="inputPassword">Password</label>
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-floating mb-3 mb-md-0">
              <input
                class="form-control"
                id="inputPasswordConfirm"
                type="password"
                placeholder="Confirm password"
                bind:value={passwordConfirm}
                required
              />
              <label for="inputPasswordConfirm">Confirm Password</label>
            </div>
          </div>
        </div>
        <div class="mt-4 mb-0">
          <div class="d-grid">
            <button class="btn btn-primary btn-block" type="submit"
              >Create Account</button
            >
          </div>
        </div>
      </form>
    </div>
    <div class="card-footer text-center py-3">
      <div class="small">
        <a href="/auth/login">Have an account? Go to login</a>
      </div>
    </div>
  </div>
</div>
