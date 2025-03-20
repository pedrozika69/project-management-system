import { writable } from 'svelte/store';

interface User {
  email: string;
}

interface AuthState {
  isAuthenticated: boolean;
  user: User | null;
  token: string | null;
}

export const authStore = writable<AuthState>({
  isAuthenticated: false,
  user: null,
  token: null,
});