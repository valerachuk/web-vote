namespace WebVote.Constants
{
  public static class AuthorizeRoles
  {
    public const string ADMIN = UserRoles.ADMIN;
    public const string MANAGER_ADMIN = UserRoles.ADMIN + ", " + UserRoles.MANAGER;
    public const string MANAGER_ADMIN_VOTER = UserRoles.ADMIN + ", " + UserRoles.MANAGER + ", " + UserRoles.VOTER;
  }
}
