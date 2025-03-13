<script lang="ts">
    import { Link } from 'svelte-routing';
    // svelte-ignore export_let_unused
    export let user: { email: string } | null = null;
</script>

<main>
    <h1>Welcome to the Home Page</h1>
    {#if user}
        <p>Hello, {user.email}!</p>
    {:else}
        <p>Please <Link to="/login">Login</Link> or <Link to="/register">Register</Link></p>
    {/if}
</main>