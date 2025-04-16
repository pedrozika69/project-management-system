<script lang="ts">
	import { onMount } from 'svelte';
	import { token } from '$lib/stores/user';
	import { get } from 'svelte/store';

	type Task = {
		id: number;
		title: string;
		description: string;
		dueDate: string;
		status: number;
		projectId: number;
		project: any;
	};

	let tasks: Task[] = [];

	onMount(async () => {
		const jwt = get(token); // 🔑 get token from store

		if (!jwt) {
			console.error('❌ No token found in store. User must log in.');
			return;
		}

		const response = await fetch('http://localhost:5086/api/task', {
			headers: {
				Authorization: `Bearer ${jwt}`
			}
		});

		if (response.ok) {
			tasks = await response.json();
		} else {
			console.error('❌ Failed to fetch tasks');
		}
	});
</script>

<h1 class="text-3xl font-bold text-center mt-10">Your Tasks</h1>

{#if tasks.length === 0}
	<p class="text-center mt-4 text-gray-500">No tasks found.</p>
{:else}
	<div class="max-w-4xl mx-auto mt-6">
		<ul class="space-y-4">
			{#each tasks as task}
				<li class="p-4 border rounded-lg shadow-sm hover:shadow-md transition">
					<h2 class="text-xl font-semibold">{task.title}</h2>
					<p class="text-gray-600 mt-1">{task.description}</p>
					<p class="text-sm text-gray-400 mt-2">Due: {new Date(task.dueDate).toLocaleDateString()}</p>
				</li>
			{/each}
		</ul>
	</div>
{/if}
