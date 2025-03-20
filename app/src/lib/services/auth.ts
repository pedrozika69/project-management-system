import { authStore } from "../stores/authStore";
import { get } from "svelte/store";

const API_URL = "http://localhost:5000/auth";

export const register = async (email: string, password: string) => {
  const response = await fetch(`${API_URL}/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  if (response.ok) {
    const data = await response.json();
    authStore.set({
      isAuthenticated: true,
      token: data.token,
      user: { email: "" },
    });
  } else {
    throw new Error("Registration failed");
  }
};

export const login = async (email: string, password: string) => {
  const response = await fetch(`${API_URL}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  if (response.ok) {
    const data = await response.json();
    authStore.set({
      isAuthenticated: true,
      user: { email },
      token: data.token,
    });
  } else {
    throw new Error("Login failed");
  }
};

export const logout = () => {
  authStore.set({ isAuthenticated: false, user: null, token: null });
};
