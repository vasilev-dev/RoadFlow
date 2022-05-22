type User = {
    id: string;
    username: string;
    email: string;
    role: UserRole;
};

type UserRole = 'Root' | 'Admin' | 'User';

export default User;
