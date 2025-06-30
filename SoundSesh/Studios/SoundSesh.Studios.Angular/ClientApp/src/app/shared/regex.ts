export const RegularExpressions = {
  zipCode: /^[0-9]{5}(?:-[0-9]{4})?$/,
  password: /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})/,
  phoneNumber: /^\d{10}$/,
  email: /[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/
};
