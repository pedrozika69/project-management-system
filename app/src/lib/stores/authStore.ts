import { browser } from '$app/environment';
import { writable } from 'svelte/store';

interface User {
  email: string;
}

interface AuthState {
  isAuthenticated: boolean;
  user: User | null;
  token: string | null;
}

const loadInitialState = (): AuthState => {

  if(!browser){
    return {isAuthenticated: false, user: null, token: null};
  }

  const token = localStorage.getItem('token');
  const user = localStorage.getItem('user');

  return {
    isAuthenticated: !!token,
    user: user ? JSON.parse(user) : null,
    token: token || null
  }
};

export const authStore = writable<AuthState>(loadInitialState());


export const setAuthState = (state: AuthState) => {
  if(state.token){
    localStorage.setItem('token', state.token);
    localStorage.setItem('user', JSON.stringify(state.user));
  } else {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }
  authStore.set(state);
}

export const clearAuthState = () => {
  localStorage.clear();
  authStore.set({isAuthenticated: false, user: null, token: null})
}


// SSR =? Server Side Rendering 
// good for SEO
// SPA, the render happens in client side