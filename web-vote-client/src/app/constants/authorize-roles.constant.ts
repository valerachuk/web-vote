import { UserRole } from './user-roles.enum';

export const AUTHORIZE_ROLES = {
  unauthorized: [UserRole.Unauthorized],
  voter: [UserRole.Voter],
  manager: [UserRole.Manager],
  admin: [UserRole.Admin],
  managerAdmin: [UserRole.Manager, UserRole.Admin],
};
