<script lang="ts">
	import { goto } from '$app/navigation';
	import { token } from '$lib/stores/user';

	let email = '';
	let password = '';
	let error: string | null = null;

	async function handleLogin() {
		error = null;

		try {
			const res = await fetch('http://localhost:5086/api/auth/login', {
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({ email, password })
			});

			if (!res.ok) {
				const body = await res.text();
				throw new Error(body || 'Login failed');
			}

			const data = await res.json();
			token.set(data.token); // ✅ store token in memory

			goto('/'); // ✅ redirect to homepage (task list)
		} catch (err: any) {
			error = err.message;
		}
	}
</script>

<h1 class="text-3xl font-bold mb-4">Login</h1>

{#if error}
	<p class="text-red-500 mb-2">Error: {error}</p>
{/if}

<form on:submit|preventDefault={handleLogin} class="space-y-4 max-w-sm">
	<div>
		<label for="email" class="block text-sm font-medium mb-1">Email</label>
		<input id="email" type="email" bind:value={email} class="w-full px-3 py-2 border rounded" required />
	</div>

	<div>
		<label for="password" class="block text-sm font-medium mb-1">Password</label>
		<input id="password" type="password" bind:value={password} class="w-full px-3 py-2 border rounded" required />
	</div>

	<button type="submit" class="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700">
		Login
	</button>
</form>
