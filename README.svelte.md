# Quick Guide to Svelte Development

This guide provides a quick overview of the main commands, principles, and tips for developing applications with Svelte.

---

## **1. Core Principles**

### **Components**
- Svelte apps are built using **components**.
- Each component is a `.svelte` file that combines HTML, CSS, and JavaScript.
- Example:
  ```svelte
  <script>
    let name = 'World';
  </script>

  <style>
    h1 {
      color: blue;
    }
  </style>

  <h1>Hello {name}!</h1>
  ```

### **Reactive Declarations**
- Svelte uses **reactive statements** to automatically update the DOM when data changes.
- Use `$:` to create reactive statements:
  ```svelte
  <script>
    let count = 0;
    $: doubled = count * 2;
  </script>

  <button on:click={() => count++}>Count: {count}</button>
  <p>Doubled: {doubled}</p>
  ```

### **Props**
- Pass data to child components using **props**.
- Example:
  ```svelte
  <!-- Parent.svelte -->
  <Child name="Svelte" />

  <!-- Child.svelte -->
  <script>
    export let name;
  </script>

  <p>Hello {name}!</p>
  ```

### **Stores**
- Use **stores** for shared state management.
- Svelte provides `writable`, `readable`, and `derived` stores.
- Example:
  ```svelte
  <script>
    import { writable } from 'svelte/store';
    const count = writable(0);
  </script>

  <button on:click={() => $count++}>Count: {$count}</button>
  ```

### **Events**
- Use `on:` directives to handle DOM events.
- Example:
  ```svelte
  <button on:click={() => alert('Clicked!')}>Click me</button>
  ```

### **Bindings**
- Use `bind:` to create two-way bindings.
- Example:
  ```svelte
  <input bind:value={name} />
  ```

---

## **2. Main Commands**

### **SvelteKit (Recommended)**
- SvelteKit is the official framework for building Svelte apps.
- Create a new SvelteKit project:
  ```bash
  npm create vite@latest svelte-app -- --template svelte-ts
  cd project-name
  npm install
  ```
- Start the development server:
  ```bash
  npm run dev
  ```
- Build the project for production:
  ```bash
  npm run build
  ```
- Preview the production build:
  ```bash
  npm run preview
  ```

### **Standalone Svelte**
- For standalone Svelte projects, use **Vite**:
  ```bash
  npm create vite@latest project-name --template svelte
  cd project-name
  npm install
  npm run dev
  ```

---

## **3. Tips for Development**

### **Organize Your Code**
- Group related components into folders (e.g., `components/`, `routes/`).
- Use **stores** for shared state instead of prop drilling.

### **Use Actions**
- Use **actions** to add reusable behavior to elements.
- Example:
  ```svelte
  <script>
    function longpress(node, duration) {
      let timeout;

      function handleMouseDown() {
        timeout = setTimeout(() => {
          node.dispatchEvent(new CustomEvent('longpress'));
        }, duration);
      }

      function handleMouseUp() {
        clearTimeout(timeout);
      }

      node.addEventListener('mousedown', handleMouseDown);
      node.addEventListener('mouseup', handleMouseUp);

      return {
        destroy() {
          node.removeEventListener('mousedown', handleMouseDown);
          node.removeEventListener('mouseup', handleMouseUp);
        },
      };
    }
  </script>

  <button use:longpress={500} on:longpress={() => alert('Long press!')}>
    Press and hold
  </button>
  ```

### **Optimize Performance**
- Svelte is highly optimized out of the box, but you can:
  - Use `{#key}` blocks to reset state when a value changes.
  - Use `{#await}` blocks for asynchronous data loading.

### **Testing**
- Use **Vitest** or **Jest** for unit testing.
- Use **Playwright** or **Cypress** for end-to-end testing.

### **Debugging**
- Use browser developer tools for debugging.
- Svelte's compiled output is close to the source code, making debugging straightforward.

### **Stay Updated**
- Svelte is actively developed. Follow the [Svelte blog](https://svelte.dev/blog) for updates.

---

## **4. Common Pitfalls**

- **Reactivity Caveats**: Svelte's reactivity is based on assignments. Ensure you use assignments (`=`) to trigger updates.
- **Store Subscriptions**: Always unsubscribe from stores in `onDestroy` to avoid memory leaks.
- **CSS Scoping**: Svelte scopes CSS by default. Use `:global()` for global styles.

---

## **5. Useful Resources**

- [Svelte Documentation](https://svelte.dev/docs)
- [SvelteKit Documentation](https://kit.svelte.dev/docs)
- [Svelte Tutorial](https://svelte.dev/tutorial/basics)
- [Svelte REPL](https://svelte.dev/repl) (Experiment with Svelte online)
- [Svelte Society](https://sveltesociety.dev/) (Community resources and tools)

---

This guide covers the essentials to get started with Svelte development. Happy coding! 🚀