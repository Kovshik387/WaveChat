import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface AccountState {
  username: string;
  isLoggedIn: boolean;
}

const initialState: AccountState = {
  username: '',
  isLoggedIn: false,
};

const accountSlice = createSlice({
  name: 'account',
  initialState,
  reducers: {
    login: (state, action: PayloadAction<string>) => {
      state.username = action.payload;
      state.isLoggedIn = true;
    },
    logout: (state) => {
      state.username = '';
      state.isLoggedIn = false;
    },
  },
});

export const { login, logout } = accountSlice.actions;

export default accountSlice.reducer;
